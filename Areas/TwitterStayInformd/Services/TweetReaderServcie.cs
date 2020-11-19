using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hangfire;
using HomeAutomation.Areas.TwitterStayInformd.Helpers;
using HomeAutomation.Areas.TwitterStayInformd.Interfaces;
using HomeAutomation.Areas.TwitterStayInformd.Modals;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.TwitterStayInformd;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HomeAutomation.Areas.TwitterStayInformd.Services
{
  public class TweetReaderServcie : ITweetReaderServcie
  {
    private readonly IConfiguration _config;
    private readonly HttpClient _client;
    private readonly TwitterStayInformdContext _context;
    private readonly ITelegramService _telegramService;
    private readonly string _baseurl;
    public TweetReaderServcie(IConfiguration config, IHttpClientFactory clientFactory, TwitterStayInformdContext context, ITelegramService telegramService)
    {
      _config = config;
      _client = clientFactory.CreateClient();
      _context = context;
      _telegramService = telegramService;
      _baseurl = "https://api.twitter.com/1.1/";
    }
    public async Task<(string, bool, object)> Start(Guid taskId)
    {
      var twitterTask = await _context.TwitterTask.FirstOrDefaultAsync(x => x.Id == taskId);
      if(twitterTask == null)
      {
        return ("task not found", false, null);
      }
      await CheckUser(twitterTask);
      RecurringJob.AddOrUpdate(taskId.ToString(), () => this.CheckUser(twitterTask), twitterTask.CronTime);

      return ("Started", true, null);
    }

    public void Stop(Guid taskId)
    {
      RecurringJob.RemoveIfExists(taskId.ToString());
    }

    public async Task<bool> CreateNewTask(TwitterTask task)
    {
      if (String.IsNullOrWhiteSpace(task.User) || String.IsNullOrWhiteSpace(task.CronTime) || String.IsNullOrWhiteSpace(task.BodId) || String.IsNullOrWhiteSpace(task.ChatId))
      {
        return false;
      }
      task.BodId = StringCipher.encodingWinDataProtection(task.BodId);
      task.ChatId = StringCipher.encodingWinDataProtection(task.ChatId);;
      _context.Add(task);
      await _context.SaveChangesAsync();
      return true;
    }

    private async Task<IEnumerable<TweetResponse>> GetTweetsFromUser(string user, int numberOfTweets)
    {
      var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseurl}statuses/user_timeline.json?screen_name={user}&count={numberOfTweets}&exclude_replies=true");
      request.Headers.Add("Authorization", $"Bearer {_config.GetValue<string>("Twitter:BearerToken")}");

      var response = await _client.SendAsync(request);
      IEnumerable<TweetResponse> tweets = new List<TweetResponse>();

      if (response.IsSuccessStatusCode)
      {
        using var responseStream = await response.Content.ReadAsStreamAsync();
        tweets = await JsonSerializer.DeserializeAsync<IEnumerable<TweetResponse>>(responseStream);
      }

      return tweets;
    }

    private async Task<bool> CheckIfTweetExist(string id)
    {
      var tweet = await _context.Tweet.FirstOrDefaultAsync(x => x.Id == id);
      if (tweet == null) return false;
      return true;
    }

    private async Task AddTweetToDb(TweetResponse tweet)
    {
      var newTweet = new Tweet()
      {
        CreatedAt = tweet.CreatedAt,
        Id = tweet.Id
      };
      _context.Add(newTweet);
      await _context.SaveChangesAsync();
    }

    public async Task CheckUser(TwitterTask task)
    {
      Console.WriteLine("Check twitter user");
      var tweets = await GetTweetsFromUser(task.User, task.NumberOfTweets);
      tweets = tweets.Reverse();


      if (tweets.Count() > 0)
      {
        foreach (var tweet in tweets)
        {
          var tweetExist = await CheckIfTweetExist(tweet.Id);
          if (!tweetExist)
          {
            await AddTweetToDb(tweet);
            await _telegramService.SendMessage(task.BodId, task.ChatId, tweet.Text);
          }
        }
      }
    }
  }
}