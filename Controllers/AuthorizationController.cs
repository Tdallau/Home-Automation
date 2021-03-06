using System;
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
      if(!ModelState.IsValid || credentials.AppId == Guid.Empty) return BadRequest(new Response<string>() {
        Error = "username, password and appId are requierd",
        Success = false
      });

      return Ok(await _authorizationService.Login(credentials));
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

    [HttpPost]
    public async Task<ActionResult<Response<JWTToken>>> Refresh([FromBody] JWTToken tokens)
    {
      var newTokens = await _authorizationService.Refresh(tokens);
      if(newTokens == null) return Ok(new Response<string>() {
        Error = "Token is expired",
        Success = false,
      });

      return Ok(new Response<JWTToken>() {
        Data = newTokens,
        Success = true
      });
    }

    [HttpPost]
    public async Task<IActionResult> Logout([FromBody] JWTToken token)
    {
      await _authorizationService.Logout(token.RefreshToken);
      return Ok(new Response<string>() {
        Data = "user is logged out",
        Success = true
      });
    }
  }
}