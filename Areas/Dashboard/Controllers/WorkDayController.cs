using System;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.Dashboard.Interfaces;
using HomeAutomation.Areas.Dashboard.Models;
using HomeAutomation.Helpers;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.Dashboard.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("dashboard")]
  [Route("[area]/[controller]")]
  public class WorkDayController : ControllerBase
  {
    private readonly IWorkDayService _workDayService;
    public WorkDayController(IWorkDayService workDayService)
    {
      _workDayService = workDayService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<WorkDay>>>> GetWorkDays()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var workDays = await _workDayService.GetWorkDays(user.UserId);
      return Ok(HelperBox.DataToReponseList(true, workDays));
    }

    [HttpGet("hours")]
    public async Task<ActionResult<Response<ResponseList<object>>>> GetHouresInMonth()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var houresInMonth = await _workDayService.GetHoursInMonth(user.UserId);

      return Ok(HelperBox.DataToResponse<HouresInMonthResponse>(true, new HouresInMonthResponse() {
          HouresList = new ResponseList<object>()  {
              List = houresInMonth.Data,
              Count = houresInMonth.Data.Count(),
          },
          Keys = houresInMonth.Keys
      }));
    }

    [HttpGet("salary")]
    public async Task<ActionResult<Response<ResponseList<object>>>> GetSalaryInMonth()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var houresInMonth = await _workDayService.GetSalaryInMonth(user.UserId);

      return Ok(HelperBox.DataToResponse<HouresInMonthResponse>(true, new HouresInMonthResponse() {
          HouresList = new ResponseList<object>()  {
              List = houresInMonth.Data,
              Count = houresInMonth.Data.Count(),
          },
          Keys = houresInMonth.Keys
      }));
    }

    [HttpPost]
    public async Task<ActionResult<Response<WorkDay>>> CreateWorkDay([FromBody] WorkDay workDayData)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var workDay = await _workDayService.CreateWorkDay(user.UserId, workDayData);
      return Ok(HelperBox.DataToResponse(true, workDay));
    }
  }
}