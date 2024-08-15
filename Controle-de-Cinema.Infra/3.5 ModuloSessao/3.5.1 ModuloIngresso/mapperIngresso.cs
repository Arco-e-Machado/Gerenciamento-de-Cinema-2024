using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.ModuloSessao;

public class mapperIngresso : IEntityTypeConfiguration<Ingresso>
{
    public void Configure(EntityTypeBuilder<Ingresso> ingressoBuilder)
    {
        ingressoBuilder.ToTable("TBIngresso");

        ingressoBuilder.Property(i => i.Id)
        .IsRequired()
        .ValueGeneratedOnAdd();


        ingressoBuilder.Property(i => i.Valor)
        .IsRequired()
        .HasColumnType("decimal");

        ingressoBuilder.Property(i => i.Status)
        .IsRequired()
        .HasColumnType("bit");

        ingressoBuilder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


    }
}