//this class has the role to easily find other services.
//you don't have to build mountains of code in the Startup.cs

using HotelListing.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            // enforce the user to have an unique e-mail.
            //find additional enforces in q.User.Conditions
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            // all the services will come with a special builder class
            // the builder object provides anything you need to buid-up services
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY"); // what you already set by the cmd command(admin rights): setx KEY 'ffc632ce-0053-4bab-8077-93a4d14caaad'--> you can replace this with whatever you want /M

            services.AddAuthentication(options =>
            {
                // means: add authentication for the app, and the default scheme I want is "JwtBearerDefaults".
                // in the token validation params, you set different token validation attributes
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                

            })
                .AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }
    }
}
