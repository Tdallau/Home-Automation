using System;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Helpers;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HomeAutomation.Services
{
  public class AuthorizationService : IAuthorizationService
  {
    private readonly MainContext _context;
    private readonly IConfiguration _config;
    private readonly IPasswordHasher<User> _passwordHasher;
    public AuthorizationService(MainContext context, IPasswordHasher<User> passwordHasher, IConfiguration config)
    {
      _context = context;
      _config = config;
      _passwordHasher = passwordHasher;
    }
    public async Task<Response<LoginResponse>> Login(Credentials credentials)
    {
      var user = await _context.User.FirstOrDefaultAsync(user => user.Email.ToLower() == credentials.Email.ToLower());
      if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, credentials.Password) == PasswordVerificationResult.Failed)
      {
        return new Response<LoginResponse>()
        {
          Error = "De combinatie van email en wachtwoord komt niet voor in ons systeem",
          Success = false
        };
      }

      var myApps = await _context.UserApp.FirstOrDefaultAsync(app => app.AppId == credentials.AppId && app.UserId == user.Id);
      if (myApps == null) return new Response<LoginResponse>() {
        Error = "U bent niet geautoriseerd om deze app te gebruiken",
        Success = false
      };

      var userToken = new Models.UserToken()
      {
        UserId = user.Id
      };
      var token = userToken.ToToken(_config);
      token.RefreshToken = TokenHelper.GenerateRefreshToken();
      var now = DateTime.Now;

      await _context.AddAsync(new HomeAutomation.Models.Database.Authorization.UserToken()
      {
        UserId = user.Id,
        RefreshToken = token.RefreshToken,
        LastUpdated = now,
        ExpiryDate = now.AddDays(14),
        AppId = credentials.AppId
      });
      await _context.SaveChangesAsync();

      var defaultAppId = _context.UserApp.FirstOrDefault(x => x.Default).AppId;
      var response = new LoginResponse()
      {
        TokenSettings = token,
        Email = user.Email,
        Id = user.Id,
        DefaultAppId = defaultAppId,
        DefaultAppName = _context.App.FirstOrDefault(x => x.Id == defaultAppId).Name
      };
      return new Response<LoginResponse>() {
        Data = response,
        Success = true
      };
    }

    public async Task<bool> Register(Credentials credentials)
    {
      var user = new User()
      {
        Id = Guid.NewGuid(),
        Email = credentials.Email,
      };
      user.Password = _passwordHasher.HashPassword(user, credentials.Password);
      try
      {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
      }
      catch (System.Exception)
      {
        return false;
      }

      var app = new UserApp() {
        UserId = user.Id,
        AppId = credentials.AppId,
        Default = true,
      };

      await _context.AddAsync(app);
      await _context.SaveChangesAsync();

      return true;
    }

    public async Task<JWTToken> Refresh(JWTToken tokens)
    {
      var tokenInDb = await _context.UserToken.FirstOrDefaultAsync(token => token.RefreshToken == tokens.RefreshToken);
      if (tokenInDb == null || tokenInDb.ExpiryDate < DateTime.Now) return null;

      var user = Models.UserToken.FromToken(tokens.JwtToken);

      var userToken = new Models.UserToken()
      {
        UserId = user.UserId
      };
      var token = userToken.ToToken(_config);
      token.RefreshToken = TokenHelper.GenerateRefreshToken();
      var now = DateTime.Now;

      tokenInDb.LastUpdated = now;
      tokenInDb.ExpiryDate = now.AddDays(14);
      tokenInDb.RefreshToken = token.RefreshToken;

      _context.Update(tokenInDb);
      await _context.SaveChangesAsync();
      return token;
    }

    public async Task Logout(string refreshToken)
    {
      var token = await _context.UserToken.FirstOrDefaultAsync(token => token.RefreshToken == refreshToken);
      if (token == null) return;
      _context.Remove(token);
      await _context.SaveChangesAsync();
    }
  }
}