using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers;
using HomeAutomation.Hubs;
using HomeAutomation.Interfaces.Hubs;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
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
  public class ProductController : ControllerBase
  {
    private readonly IProductService _productService;
    private readonly IShoppingGroupService _shoppingGroupService;
    private readonly IHubContext<MyShoppingListHub> _shoppingListHub;

    public ProductController(IProductService productService, IShoppingGroupService shoppingGroupService, IHubContext<MyShoppingListHub> shoppingListHub)
    {
      _productService = productService;
      _shoppingGroupService = shoppingGroupService;
      _shoppingListHub = shoppingListHub;
    }

    [HttpPost("auto")]
    public async Task<ActionResult<Response<ResponseList<string>>>> GetAutoComplete([FromBody] AutoComplete autoComplete)
    {
        var options = await _productService.AutoComplete(autoComplete.text);
        return Ok(HelperBox.DataToReponseList(true, options));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
    {
      var product = await _productService.AddProduct(productRequest);
      if (product == null) return BadRequest(HelperBox.DataToResponse<object>(false, null, "winkel niet gevonden"));

      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var shoppingGroup = await _shoppingGroupService.GetShoppingGroupByUserId(user.UserId);
      await _shoppingListHub.Clients.Group(shoppingGroup.Id.ToString()).SendAsync(nameof(IMyShoppingListHub.AddProduct), productRequest.ShopId, product);

      return Ok(HelperBox.DataToResponse(true, "Product is toegevoegd aan de winkel"));
    }

    [HttpPut("{productShopId}")]
    public async Task<IActionResult> UpdateProduct(int productShopId, [FromBody] ProductRequest productRequest)
    {
      var product = await _productService.UpdateProduct(productShopId, productRequest.ShopId);
      if (product == null) return BadRequest(HelperBox.DataToResponse<object>(false, null, "winkel niet gevonden"));
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      var shoppingGroup = await _shoppingGroupService.GetShoppingGroupByUserId(user.UserId);
      await _shoppingListHub.Clients.Group(shoppingGroup.Id.ToString()).SendAsync(nameof(IMyShoppingListHub.UpdateProduct), productRequest.ShopId, product);
      return Ok(HelperBox.DataToResponse(true, "Product is geupdate aan de winkel"));
    }
  }
}