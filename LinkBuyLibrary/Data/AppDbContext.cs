using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkBuyLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(p =>
            {
                p.Property(p => p.Valor).HasColumnType("DECIMAL(5,2)");
            });
        }
    }
}
