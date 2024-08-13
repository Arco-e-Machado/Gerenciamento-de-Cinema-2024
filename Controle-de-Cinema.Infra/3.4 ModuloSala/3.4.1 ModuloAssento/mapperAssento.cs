using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.ModuloSala
{
    public class mapperAssento : IEntityTypeConfiguration<Assento>
    {
        public void Configure(EntityTypeBuilder<Assento> assentoBuilder)
        {
            assentoBuilder.ToTable("TBAssento");

            assentoBuilder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            assentoBuilder.Property(a => a.Numero)
                .IsRequired()
                .HasColumnType("varchar(100)");

            assentoBuilder.Property(a => a.Status)
                .IsRequired()
                .HasColumnType("bit");

            assentoBuilder.HasOne(x => x.usuario)
                .WithMany()
                .HasForeignKey("User_Id")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}