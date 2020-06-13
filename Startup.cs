using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyRecipes.Helpers.Mappers;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Areas.MyRecipes.Services;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models.Database.MyRecipes;
using HomeAutomation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HomeAutomation
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddDbContext<MainContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
      );
      services.AddDbContext<MyRecipesContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("MyRecipeConnection"))
      );

      // add autentication
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = "Jwt";
        options.DefaultChallengeScheme = "Jwt";
      }).AddJwtBearer("Jwt", options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateAudience = false,
          ValidateIssuer = false,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecretKey"))),
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero,
        };
      });

      // cors
      var corsBuilder = new CorsPolicyBuilder();
      corsBuilder.AllowAnyHeader();
      corsBuilder.AllowAnyMethod();
      //corsBuilder.AllowAnyOrigin(); // For anyone access.
      corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
      corsBuilder.AllowCredentials();

      services.AddCors(options =>
      {
        options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
      });

      // main project
      services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
      services.AddScoped<IAuthorizationService, AuthorizationService>();

      // my recipes
      services.AddScoped<IRecipeService, RecipeService>();
      services.AddScoped(typeof(IMapper<,>), typeof(BaseMapper<,>));
      services.AddScoped(typeof(IMapper<Recipe, RecipeResponse>), typeof(RecipeToRecipeResponse));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("SiteCorsPolicy");

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
