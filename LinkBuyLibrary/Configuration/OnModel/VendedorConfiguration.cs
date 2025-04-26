using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkBuyLibrary.Configuration.OnModel
{
    public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.DataCadastro).HasColumnType("DATETIME");
        }
    }
}