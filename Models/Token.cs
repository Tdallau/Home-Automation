using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HomeAutomation.Models
{
  public class UserToken
  {
    public Guid UserId { get; set; }
    public string Nbf { get; set; }
    public string Exp { get; set; }

    public JWTToken ToToken(IConfiguration config)
    {
      var nbf = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
      var exp = new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds();
      var claims = new Claim[]
      {
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, nbf.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, exp.ToString()),
      };

      var token = new JwtSecurityToken(
          new JwtHeader(new SigningCredentials(
              new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("SecretKey"))),
                                       SecurityAlgorithms.HmacSha256)),
          new JwtPayload(claims));

      return new JWTToken()
      {
        JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
        RefreshToken = null,
        Nbf = nbf,
        Exp = exp
      };
    }
    public static UserToken FromToken(string token)
    {
      var jwttoken = new JwtSecurityToken(token);
      Guid id =  Guid.Parse(jwttoken.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
      string role = jwttoken.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value;
      string nbf = jwttoken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Nbf).FirstOrDefault()?.Value;
      string exp = jwttoken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Exp).FirstOrDefault()?.Value;

      return new UserToken { UserId = id, Nbf = nbf, Exp = exp };
    }
  }

  public class JWTToken
  {
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
    public long Nbf { get; set; }
    public long Exp { get; set; }
  }
}