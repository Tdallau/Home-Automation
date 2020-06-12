using System;
using System.Threading.Tasks;
using HomeAutomation.Helpers;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
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
    public async Task<LoginResponse> Login(Credentials credentials)
    {
      var user = await _context.User.FirstOrDefaultAsync(user => user.Email == credentials.Email);
      if(user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, credentials.Password) == PasswordVerificationResult.Failed) return null;

      var userToken = new Models.UserToken() {
        UserId = user.Id
      };
      var token = userToken.ToToken(_config);
      token.RefreshToken = TokenHelper.GenerateRefreshToken();
      var now = DateTime.Now;

      await _context.AddAsync(new HomeAutomation.Models.Database.Authorization.UserToken() {
        UserId = user.Id,
        RefreshToken = token.RefreshToken,
        LastUpdated = now,
        ExpiryDate = now.AddDays(7)
      });
      await _context.SaveChangesAsync();

      var response = new LoginResponse() {
        JwtToken = token,
        Email = user.Email,
        Id = user.Id,
      };
      return response;
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
      return true;
    }
  }
}