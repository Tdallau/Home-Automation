using System.Threading.Tasks;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class AuthorizationController : ControllerBase
  {
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
      _authorizationService = authorizationService;
    }

    [HttpPost]
    public async Task<ActionResult<Response<LoginResponse>>> Login([FromBody] Credentials credentials)
    {
      if(!ModelState.IsValid) return BadRequest(new Response<string>() {
        Error = "username and password are requierd",
        Success = false
      });

      var user = await _authorizationService.Login(credentials);

      if(user == null) return Forbid();

      return Ok(new Response<LoginResponse>() {
        Data = user,
        Success = true
      });
    }

    [HttpPost]
    public async Task<ActionResult<Response<string>>> Register([FromBody] Credentials user)
    {
      if(!ModelState.IsValid) return BadRequest(new Response<string>() {
        Error = "username and password are requierd",
        Success = false
      });
      var userCreated = await _authorizationService.Register(user);
      if(!userCreated) return Forbid();

      return Ok(new Response<string>() {
        Data = "User is created",
        Success = true
      });
    }
  }
}