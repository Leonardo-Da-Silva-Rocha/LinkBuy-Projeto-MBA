using LinkBuyLibrary.Data;
using LinkBuyLibrary.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkBuyLibrary.Configuration.Middlewares
{
    public static class AddJwt
    {
        public static void AddConfigIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
        public static void AddConfigCookieMVC(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/conta/entrar";
                options.LogoutPath = "/conta/criar-conta";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });
        }
        public static void AddConfigJwtAPI(this WebApplicationBuilder builder)
        {

            var jwtsettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtsettingsSection);

            var JwtSettings = jwtsettingsSection.Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(JwtSettings.Segredo);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = JwtSettings.Audiencia,
                        ValidIssuer = JwtSettings.Emissor,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

        }
    }
}
