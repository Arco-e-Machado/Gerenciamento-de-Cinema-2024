using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.ModuloFilme;

public class mapperFilme : IEntityTypeConfiguration<Filme>
{
    public void Configure(EntityTypeBuilder<Filme> filmeBuilder)
    {
        filmeBuilder.ToTable("TBfilme");

        filmeBuilder.Property(f => f.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        filmeBuilder.Property(f => f.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        filmeBuilder.Property(f => f.Duracao)
            .IsRequired()
            .HasColumnType("time");

        filmeBuilder.Property(f => f.Genero)
            .IsRequired()
            .HasColumnType("int");

        filmeBuilder.HasData(NewData());
    }

    private Filme[] NewData()
    {
        return
        [
            new Filme
            {
                Id = 1,
                Nome = "UP - Altas Aventuras",
                Duracao = new TimeSpan(1, 36, 0),
                Genero = EnumGeneros.Animação
            },
            new Filme
            {
                Id = 2,
                Nome = "Os Vingadores",
                Duracao = new TimeSpan(2, 23, 0),
                Genero = EnumGeneros.Ação
            },
            new Filme
            {
                Id = 3,
                Nome = "Jurassic Park",
                Duracao = new TimeSpan(2, 7, 0),
                Genero = EnumGeneros.Aventura
            },
            new Filme
            {
                Id = 4,
                Nome = "O Grande Lebowski",
                Duracao = new TimeSpan(1, 57, 0),
                Genero = EnumGeneros.Comédia
            },
            new Filme
            {
                Id = 5,
                Nome = "A Lista de Schindler",
                Duracao = new TimeSpan(3, 15, 0),
                Genero = EnumGeneros.Drama
            },
            new Filme
            {
                Id = 6,
                Nome = "O Senhor dos Anéis: A Sociedade do Anel",
                Duracao = new TimeSpan(2, 58, 0),
                Genero = EnumGeneros.Fantasia
            },
            new Filme
            {
                Id = 7,
                Nome = "O Exorcista",
                Duracao = new TimeSpan(2, 2, 0),
                Genero = EnumGeneros.Terror
            },
            new Filme
            {
                Id = 8,
                Nome = "Diário de uma Paixão",
                Duracao = new TimeSpan(2, 4, 0),
                Genero = EnumGeneros.Romance
            },
            new Filme
            {
                Id = 9,
                Nome = "O Silêncio dos Inocentes",
                Duracao = new TimeSpan(1, 58, 0),
                Genero = EnumGeneros.Suspense
            },
            new Filme
            {
                Id = 10,
                Nome = "Gladiador",
                Duracao = new TimeSpan(2, 35, 0),
                Genero = EnumGeneros.Histórico
            },
        ];
    }
}