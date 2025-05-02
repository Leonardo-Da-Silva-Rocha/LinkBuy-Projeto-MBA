using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LinkBuyLibrary.Configuration.Middlewares
{
    public static class AddInterfaces
    {
        public static void AddInterfacesProgram(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<VendedorService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ProdutoService>();
        }
    }
}
