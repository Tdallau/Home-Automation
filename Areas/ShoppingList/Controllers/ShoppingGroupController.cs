using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.ShoppingList.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("shoppingList")]
  [Route("[area]/[controller]")]
  public class ShoppingGroupController : ControllerBase
  {
    private readonly IShoppingGroupService _shoppingGroupService;
    public ShoppingGroupController(IShoppingGroupService shoppingGroupService)
    {
      _shoppingGroupService = shoppingGroupService;
    }

    // POST: shoppingList/shoppingGroup
    [HttpPost]
    public async Task<ActionResult<Response<ShoppingGroup>>> AddShoppingGroup([FromBody] ShoppingGroupRequest shoppingGroupRequest)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var newShoppingGroup = await _shoppingGroupService.CreateShoppingGroup(user.UserId, shoppingGroupRequest);
      if (newShoppingGroup == null) return BadRequest(HelperBox.DataToResponse<ShoppingGroup>(false, null, "Er is geen naam gegeven aan deze winkelgroep"));
      return Ok(HelperBox.DataToResponse(true, newShoppingGroup));
    }

    [HttpPut]
    public async Task<IActionResult> SetActiveShoppingGroup([FromBody] ShoppingGroupRequest shoppingGroupRequest)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      await _shoppingGroupService.SetActiveShoppingGroup(user.UserId, shoppingGroupRequest);
      return Ok(HelperBox.DataToResponse<string>(true, "winkel groep is nu active"));
    }

    [HttpGet]
    public async Task<ActionResult<Response<ShoppingGroup>>> GetActiveShoppingGroup()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);
      var shoppingGroup = await _shoppingGroupService.GetShoppingGroupByUserId(user.UserId);
      if(shoppingGroup == null) return BadRequest(HelperBox.DataToResponse<ShoppingGroup>(false, null, "Winkelgroep niet gevonden"));
      return Ok(HelperBox.DataToResponse(true, shoppingGroup));
    }

  }
}