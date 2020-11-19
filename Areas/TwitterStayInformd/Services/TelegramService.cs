using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HomeAutomation.Areas.TwitterStayInformd.Helpers;
using HomeAutomation.Areas.TwitterStayInformd.Interfaces;
using HomeAutomation.Areas.TwitterStayInformd.Modals;
using Microsoft.Extensions.Configuration;

namespace HomeAutomation.Areas.TwitterStayInformd.Services
{
  public class TelegramService : ITelegramService
  {
    private readonly IConfiguration _config;
    private readonly HttpClient _client;
    private readonly string _baseurl;
    public TelegramService(IConfiguration config, IHttpClientFactory clientFactory)
    {
      _config = config;
      _client = clientFactory.CreateClient();

      _baseurl = "https://api.telegram.org/";
    }

    public async Task<object> SendMessage(string bodId, string chatId, string text)
    {
      var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseurl}bot{StringCipher.decodingWinDataProtection(bodId)}/sendMessage");
      var body = new TelegramChat()
      {
        ChatId = StringCipher.decodingWinDataProtection(chatId),
        Text = text
      };

      request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
      var response = await _client.SendAsync(request);

      if (response.IsSuccessStatusCode)
      {
        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<object>(responseStream);
      }
      return null;
    }
  }
}