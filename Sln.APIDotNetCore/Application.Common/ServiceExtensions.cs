using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Common
{
    public static class ServiceExtensions
    {
        //public static void ConfigureCORS(this IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("CorsPolicy",
        //            builder => builder.AllowAnyOrigin()
        //            .AllowAnyMethod()
        //            .AllowAnyHeader()
        //            .AllowCredentials());
        //    });
        //}

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration _config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = _config["Jwt:issuer"],
                    ValidAudience = _config["Jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                };
            });

        }


    }
}
