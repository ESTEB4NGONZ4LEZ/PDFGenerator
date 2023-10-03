
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente");

            builder.Property(x => x.Nombres)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.Apellidos)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.NroCedula)
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(x => x.Telefono)
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}