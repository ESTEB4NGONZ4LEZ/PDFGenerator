
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class CompraConfig : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("compra");

            builder.HasOne(a => a.Cliente)
            .WithMany(e => e.Compras)
            .HasForeignKey(i => i.IdCliente)
            .IsRequired();

            builder.HasOne(a => a.Producto)
            .WithMany(e => e.Compras)
            .HasForeignKey(i => i.IdProducto)
            .IsRequired();

            builder.Property(x => x.Fecha)
            .HasColumnType("datetime")
            .IsRequired();

            builder.Property(x => x.Cantidad)
            .HasColumnType("int")
            .IsRequired();
        }
    }
}