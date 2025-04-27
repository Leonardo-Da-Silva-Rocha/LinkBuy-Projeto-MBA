using LinkBuyLibrary.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LinkBuyMvc.Configuration
{
    public static class DataBaseSelectorExtension
    {
        public static void AddDataBaseSelector(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                SQLitePCL.Batteries.Init();
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionLite"), b => b.MigrationsAssembly("LinkBuyLibrary"));
                });
              
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("LinkBuyLibrary"));
                });

            }
        }
    }
}