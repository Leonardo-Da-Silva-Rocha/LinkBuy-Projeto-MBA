using LinkBuyLibrary.Data;
using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LinkBuyMvc.Configuration
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {

        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateAsyncScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var auth = scope.ServiceProvider.GetRequiredService<AuthService>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.IsStaging())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedProducts(context, auth);
            }
        }

        public static async Task EnsureSeedProducts(AppDbContext context, AuthService service)
        {
            await AddCategorias(context);

            await AddUsuarios(context, service);

            await AddProdutos(context);
        }

        public async static Task AddCategorias(AppDbContext context)
        {
            if (context.Categorias.Any())
                return;

            await context.Categorias.AddAsync(new Categoria
            {
                Descricao = "Eletronicos",
            });

            await context.Categorias.AddAsync(new Categoria
            {
                Descricao = "Brinquedos",
            });

            await context.Categorias.AddAsync(new Categoria
            {
                Descricao = "Games",
            });
        }

        public async static Task AddUsuarios(AppDbContext context, AuthService service)
        {
            if (context.Users.Any())
                return;

            RegisterViewModel register = new RegisterViewModel();
            register.Email = "antonio@gmail.com";
            register.Nome = "Antonio";
            register.Password = "Teste@123";
            register.ConfirmPassword = "Teste@123";

            await service.Register(register);

            register = new RegisterViewModel();
            register.Email = "claudio@gmail.com";
            register.Nome = "Claudio";
            register.Password = "Teste@123";
            register.ConfirmPassword = "Teste@123";

            await service.Register(register);

            register = new RegisterViewModel();
            register.Email = "marta@gmail.com";
            register.Nome = "Marta";
            register.Password = "Teste@123";
            register.ConfirmPassword = "Teste@123";

            await service.Register(register);
        }

        public async static Task AddProdutos(AppDbContext context)
        {
            if (context.Produtos.Any())
                return;

            List<Produto> produtos = new List<Produto>();

            produtos.Add(new Produto
            {

                Descricao = "Pista hot wheels",
                Estoque = 5,
                Imagem = "pista.jpg",
                VendedorId = 1,
                CategoriaId = 2,
                Valor = 391.99m
            });

            produtos.Add(new Produto
            {
                Descricao = "Super Nintendo",
                Estoque = 12,
                Imagem = "super.jpg",
                VendedorId = 1,
                CategoriaId = 3,
                Valor = 1200.00m
            });

            produtos.Add(new Produto
            {
                Descricao = "PS5",
                Estoque = 30,
                Imagem = "ps5.jpg",
                VendedorId = 2,
                CategoriaId = 3,
                Valor = 6249.00m
            });

            produtos.Add(new Produto
            {
                Descricao = "adptador",
                Estoque = 620,
                Imagem = "adptador.jpg",
                VendedorId = 3,
                CategoriaId = 1,
                Valor = 12.00m
            });

            produtos.Add(new Produto
            {
                Descricao = "baba eletronica",
                Estoque = 420,
                Imagem = "baba.jpg",
                VendedorId = 3,
                CategoriaId = 1,
                Valor = 95.90m
            });

            produtos.Add(new Produto
            {
                Descricao = "Roku Express",
                Estoque = 10,
                Imagem = "roku.jpg",
                VendedorId = 3,
                CategoriaId = 1,
                Valor = 198.98m
            });

            produtos.Add(new Produto
            {
                Descricao = "Trem Piui",
                Estoque = 7,
                Imagem = "trem.jpg",
                VendedorId = 2,
                CategoriaId = 2,
                Valor = 7.90m
            });

            produtos.Add(new Produto
            {
                Descricao = "Pesca Maluca",
                Estoque = 33,
                Imagem = "pesca.jpg",
                VendedorId = 2,
                CategoriaId = 2,
                Valor = 49.50m
            });

            produtos.Add(new Produto
            {
                Descricao = "Jogo Genius",
                Estoque = 60,
                Imagem = "genius.jpg",
                VendedorId = 2,
                CategoriaId = 2,
                Valor = 47.90m
            });

            produtos.Add(new Produto
            {
                Descricao = "God Of War Ragnarok",
                Estoque = 120,
                Imagem = "war.jpg",
                VendedorId = 2,
                CategoriaId = 3,
                Valor = 249.90m
            });

            produtos.Add(new Produto
            {
                Descricao = "The Last Of Us Part 1",
                Estoque = 20,
                Imagem = "last.jpg",
                VendedorId = 1,
                CategoriaId = 3,
                Valor = 213.90m
            });

            await context.Produtos.AddRangeAsync(produtos);
            await context.SaveChangesAsync();
        }
    }
}
