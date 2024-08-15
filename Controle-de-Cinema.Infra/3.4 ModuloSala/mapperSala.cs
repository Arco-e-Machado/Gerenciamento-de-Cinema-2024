using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.ModuloSala;

public class mapperSala : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> salaBuilder)
    {
        salaBuilder.ToTable("TBSala");

        salaBuilder.Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        salaBuilder.Property(s => s.NumeroDaSala)
            .IsRequired()
            .HasColumnType("varchar(100)");

        salaBuilder.Property(s => s.Capacidade)
            .IsRequired()
            .HasColumnType("int");

        salaBuilder.HasMany(s => s.Assentos)
            .WithOne(a => a.Sala)
            .HasForeignKey("sala_Id")
            .OnDelete(DeleteBehavior.Cascade);

        salaBuilder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

    }

    private Sala[] NewData()
    {
        return new[]
        {
                new Sala { Id = 1, NumeroDaSala = "Pequena 01", Capacidade = 30 } ,
                new Sala { Id = 2, NumeroDaSala = "Pequena 02", Capacidade = 45 },
                new Sala { Id = 3, NumeroDaSala = "Média 01", Capacidade = 80 },
                new Sala { Id = 4, NumeroDaSala = "Média 02", Capacidade = 110 },
                new Sala { Id = 5, NumeroDaSala = "Grande 01", Capacidade = 180 },
                new Sala { Id = 6, NumeroDaSala = "Grande 02", Capacidade = 200 }
            };
    }
}
