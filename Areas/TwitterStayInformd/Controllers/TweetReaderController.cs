using System;
using System.Threading.Tasks;
using HomeAutomation.Areas.TwitterStayInformd.Interfaces;
using HomeAutomation.Models.Database.TwitterStayInformd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.TwitterStayInformd.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("twitter")]
  [Route("[area]/[controller]")]
  public class TweetReaderController : ControllerBase
  {
    private readonly ITweetReaderServcie _tweetReaderService;
    public TweetReaderController(ITweetReaderServcie tweetReaderService)
    {
      _tweetReaderService = tweetReaderService;
    }

    [HttpGet("start/{taskId}")]
    public async Task<IActionResult> Start(Guid taskId)
    {
      var status = await _tweetReaderService.Start(taskId);
      return Ok(status.Item3);
    }
    [HttpGet("stop/{taskId}")]
    public IActionResult Stop(Guid taskId)
    {
      _tweetReaderService.Stop(taskId);
      return Ok("Stopped if existed");
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TwitterTask task)
    {
      var result = await _tweetReaderService.CreateNewTask(task);
      if(!result) return NotFound();
      return Ok("Task created");
    }
  }
}