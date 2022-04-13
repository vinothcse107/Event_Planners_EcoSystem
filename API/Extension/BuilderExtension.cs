using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extension
{
      public static class BuilderExtension
      {
            public static IServiceCollection BuilderServices(this IServiceCollection services, IConfiguration config)
            {
                  services.AddDbContext<Context>(o =>
                      o.UseSqlServer(config.GetConnectionString("API")));

                  services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                  {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                              ValidateIssuerSigningKey = true,
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                              ValidateIssuer = false,
                              ValidateAudience = false
                        };
                  });

                  services.AddScoped<ITokenService, TokenService>();
                  return services;
            }
      }
}