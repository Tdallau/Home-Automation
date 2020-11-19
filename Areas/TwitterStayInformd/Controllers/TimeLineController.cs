using System.Collections.Generic;
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
  public class TimeLineController : ControllerBase
  {
      private readonly ITimelineService _timelineService;
      public TimeLineController(ITimelineService timelineService)
      {
          _timelineService = timelineService;
      }

      [HttpGet]
      public ActionResult<IEnumerable<Tweet>> GetTweets()
      {
          return Ok(_timelineService.GetTweets());
      }
  }
}