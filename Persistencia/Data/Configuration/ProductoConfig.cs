
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("producto");

            builder.Property(x => x.Referencia)
            .HasMaxLength(10)
            .IsRequired();

            builder.Property(x => x.Nombre)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.Fecha)
            .HasColumnType("date")
            .IsRequired();

            builder.Property(x => x.Stock)
            .HasColumnType("int")
            .IsRequired();

            builder.Property(x => x.PrecioCompra)
            .HasColumnType("decimal(8, 2)")
            .IsRequired();

            builder.Property(x => x.PrecioVenta)
            .HasColumnType("decimal(8, 2)")
            .IsRequired();
        }
    }
}