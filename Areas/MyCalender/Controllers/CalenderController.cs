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

    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<HomeAutomation.Models.Database.MyCalender.MyCalender>>>> GetCalender()
    {
      return Ok(HelperBox.DataToReponseList(true, await _calenderService.GetCalenders()));
    }

    [HttpPost]
    public async Task<ActionResult<Response<HomeAutomation.Models.Database.MyCalender.MyCalender>>> CreateNewCalender([FromBody] MyCalenderRequest newCalender)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var calender = await _calenderService.CreateNewCalender(newCalender, user.UserId);
      if(calender == null) return BadRequest(HelperBox.DataToResponse<object>(false, null, "Naam en Filenaam zijn verplicht"));
      return Ok(HelperBox.DataToResponse(true, calender));
    }
  }
}