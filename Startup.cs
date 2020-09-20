using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAutomation.Areas.Dashboard.Interfaces;
using HomeAutomation.Areas.Dashboard.Servcies;
using HomeAutomation.Areas.MyCalender.Interfaces;
using HomeAutomation.Areas.MyCalender.services;
using HomeAutomation.Areas.MyRecipes.Helpers.Mappers;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Areas.MyRecipes.Services;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Services;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Hubs;
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
      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen();

      services.AddControllers();

      services.AddDbContext<MainContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
      );
      services.AddDbContext<MyRecipesContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("MyRecipeConnection"))
      );
      services.AddDbContext<MyShoppingListContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("MyShoppingListConnection"))
      );
      services.AddDbContext<DashboardContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("DashboardConnection"))
      );
      services.AddDbContext<MyCalenderContext>(
        opt => opt.UseNpgsql(Configuration.GetConnectionString("MyCalenderConnection"))
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
      corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:8000", "https://recepten.dallau.com"); // for a specific url. Don't add a forward slash on the end!
      corsBuilder.AllowCredentials();

      services.AddCors(options =>
      {
        options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
      });

      // main project
      services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
      services.AddScoped<IAuthorizationService, AuthorizationService>();
      services.AddScoped<IUserService, UserService>();
      services.AddSignalR();

      // my recipes
      services.AddScoped<IRecipeService, RecipeService>();
      services.AddScoped(typeof(IMapper<,>), typeof(BaseMapper<,>));
      services.AddScoped(typeof(IMapper<Recipe, RecipeResponse>), typeof(RecipeToRecipeResponse));

      // my shoppinglist
      services.AddScoped<IShopService, ShopService>();
      services.AddScoped<IShoppingGroupService, ShoppingGroupService>();
      services.AddScoped<IProductService, ProductService>();

      // dashboard
      services.AddScoped<IWorkDayService, WorkDayService>();

      // my calender
      services.AddScoped<ICalenderService, CalenderService>();

      // home automation
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("SiteCorsPolicy");

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<MyShoppingListHub>("/hub/shoppingList");
      });
    }
  }
}
