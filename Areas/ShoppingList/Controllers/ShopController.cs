using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers;
using HomeAutomation.Hubs;
using HomeAutomation.Interfaces.Hubs;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HomeAutomation.Areas.ShoppingList.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("shoppingList")]
  [Route("[area]/[controller]")]
  public class ShopController : ControllerBase
  {
    private readonly IShopService _shopService;
    private readonly IHubContext<MyShoppingListHub> _shoppingListHub;
    private readonly IShoppingGroupService _shoppingGroupService;

    public ShopController(IShopService shopService, IShoppingGroupService shoppingGroupService, IHubContext<MyShoppingListHub> shoppingListHub)
    {
      _shopService = shopService;
      _shoppingGroupService = shoppingGroupService;
      _shoppingListHub = shoppingListHub;
    }

    // GET: shoppingList/shop
    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<Shop>>>> GetShops()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var shoppingGroup = await _shoppingGroupService.GetShoppingGroupByUserId(user.UserId);
      var shops = await _shopService.GetShops(shoppingGroup.Id);
      return Ok(HelperBox.DataToReponseList(true, shops));
    }

    [HttpPost]
    public async Task<ActionResult<Response<Shop>>> CreateShop([FromBody] ShopRequest shopRequest) 
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var shoppingGroup = await _shoppingGroupService.GetShoppingGroupByUserId(user.UserId);

      var shop = await _shopService.CreateShop(shoppingGroup.Id, shopRequest);
      if(shop == null) return BadRequest(HelperBox.DataToResponse<Shop>(false, null, "Een winkel heeft een naam en logo nodig"));
      shop.Products = new List<ProductForShop>();

      await _shoppingListHub.Clients.Group(shoppingGroup.Id.ToString()).SendAsync(nameof(IMyShoppingListHub.NewShopCreated), shop);

      return Ok(HelperBox.DataToResponse(true, shop));
    }
  }
}