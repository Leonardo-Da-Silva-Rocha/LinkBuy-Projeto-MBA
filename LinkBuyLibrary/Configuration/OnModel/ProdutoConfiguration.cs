using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkBuyLibrary.Configuration.OnModel
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(p => p.Valor).HasColumnType("DECIMAL(5,2)");
            builder.Property(p => p.Descricao).HasColumnType("VARCHAT(50)");

            builder.HasOne(p => p.Categoria)
                      .WithMany(c => c.Produtos)
                      .HasForeignKey(p => p.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
        }
    }
}