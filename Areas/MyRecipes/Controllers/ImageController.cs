using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeAutomation.Areas.MyRecipes.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("myrecipes")]
  [Route("[area]/[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly IWebHostEnvironment _enviroment;
    private readonly ILogger<ImageController> _logger;
    public ImageController(IWebHostEnvironment environment, ILogger<ImageController> logger)
    {
      _enviroment = environment;
      _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("{recipeId}/{imageName}")]
    public IActionResult GetImage(int recipeId, string imageName)
    {
        // trying to solve a problem
      var path = Path.Combine(_enviroment.ContentRootPath, $"images/{recipeId}/{imageName}");
      Console.WriteLine(path);
      if (!System.IO.File.Exists(path))
      {
        return NotFound();
      }
      var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
      return File(fs, "image/png");
    }
  }
}