using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyCalender.Interfaces;
using HomeAutomation.Areas.MyCalender.Models;
using HomeAutomation.Helpers;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.MyCalender.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("mycalender")]
  [Route("[area]/[controller]")]
  public class CalenderController : ControllerBase
  {
    private readonly ICalenderService _calenderService;

    public CalenderController(ICalenderService calenderService)
    {
      _calenderService = calenderService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<HomeAutomation.Models.Database.MyCalender.MyCalender>>>> GetCalender()
    {
      return Ok(HelperBox.DataToReponseList(true, await _calenderService.GetCalenders()));
    }

    [HttpGet("my")]
    public async Task<ActionResult<Response<ResponseList<HomeAutomation.Models.Database.MyCalender.MyCalender>>>> GetMyCalenders()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);
      return Ok(HelperBox.DataToReponseList(true, await _calenderService.GetCalenders(user.UserId)));
    }

    [HttpPost]
    public async Task<ActionResult<Response<HomeAutomation.Models.Database.MyCalender.MyCalender>>> CreateNewCalender([FromBody] MyCalenderRequest newCalender)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var calender = await _calenderService.CreateNewCalender(newCalender, user.UserId);
      if (calender == null) return BadRequest(HelperBox.DataToResponse<object>(false, null, "Naam en Filenaam zijn verplicht"));
      return Ok(HelperBox.DataToResponse(true, calender));
    }

    [AllowAnonymous]
    [HttpPost("search")]
    public async Task<ActionResult<Response<SearchResponse>>> SearchCalender([FromBody] SearchRequest searchSettings)
    {
      var response = await _calenderService.Search(searchSettings);
      if (response == null) return BadRequest(HelperBox.DataToResponse<string>(false, null, "Er moet minimaal een searchString mee worden gegeven"));
      return Ok(HelperBox.DataToResponse(true, response));
    }
  }
}