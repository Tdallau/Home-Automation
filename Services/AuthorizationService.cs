using System;
using System.Threading.Tasks;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Database.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Services
{
  public class AuthorizationService : IAuthorizationService
  {
    private readonly MainContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    public AuthorizationService(MainContext context, IPasswordHasher<User> passwordHasher)
    {
      _context = context;
      _passwordHasher = passwordHasher;
    }
    public async Task<User> Login(Credentials credentials)
    {
      var user = await _context.User.FirstOrDefaultAsync(user => user.Email == credentials.Email);
      if(user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, credentials.Password) == PasswordVerificationResult.Failed) return null;
      return user;
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