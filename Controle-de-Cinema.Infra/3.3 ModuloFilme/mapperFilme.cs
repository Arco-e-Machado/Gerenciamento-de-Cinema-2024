using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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

        filmeBuilder.Property(f => f.ImagemUrl)
            .IsRequired()
            .HasColumnType("varchar(600)");

        filmeBuilder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

    }

    private Filme[] NewData()
    {
        return new Filme[]
        {
            new Filme
            {
                Id = 1,
                Nome = "UP - Altas Aventuras",
                Duracao = new TimeSpan(1, 36, 0),
                Genero = EnumGeneros.Animação,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/0/05/Up_%282009_film%29.jpg"
            },
            new Filme
            {
                Id = 2,
                Nome = "Os Vingadores",
                Duracao = new TimeSpan(2, 23, 0),
                Genero = EnumGeneros.Ação,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/f/f9/TheAvengers2012Poster.jpg"
            },
            new Filme
            {
                Id = 3,
                Nome = "Jurassic Park",
                Duracao = new TimeSpan(2, 7, 0),
                Genero = EnumGeneros.Aventura,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/e/e7/Jurassic_Park_poster.jpg"
            },
            new Filme
            {
                Id = 4,
                Nome = "O Grande Lebowski",
                Duracao = new TimeSpan(1, 57, 0),
                Genero = EnumGeneros.Comédia,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/3/35/Biglebowskiposter.jpg"
            },
            new Filme
            {
                Id = 5,
                Nome = "A Lista de Schindler",
                Duracao = new TimeSpan(3, 15, 0),
                Genero = EnumGeneros.Drama,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/3/38/Schindler%27s_List_movie.jpg"
            },
            new Filme
            {
                Id = 6,
                Nome = "O Senhor dos Anéis: A Sociedade do Anel",
                Duracao = new TimeSpan(2, 58, 0),
                Genero = EnumGeneros.Fantasia,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/8/87/Ringstrilogyposter.jpg"
            },
            new Filme
            {
                Id = 7,
                Nome = "O Exorcista",
                Duracao = new TimeSpan(2, 2, 0),
                Genero = EnumGeneros.Terror,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/6/6b/Exorcist_ver2.jpg"
            },
            new Filme
            {
                Id = 8,
                Nome = "Diário de uma Paixão",
                Duracao = new TimeSpan(2, 4, 0),
                Genero = EnumGeneros.Romance,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/Posternotebook.jpg"
            },
            new Filme
            {
                Id = 9,
                Nome = "O Silêncio dos Inocentes",
                Duracao = new TimeSpan(1, 58, 0),
                Genero = EnumGeneros.Suspense,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/The_Silence_of_the_Lambs_poster.jpg"
            },
            new Filme
            {
                Id = 10,
                Nome = "Gladiador",
                Duracao = new TimeSpan(2, 35, 0),
                Genero = EnumGeneros.Histórico,
                ImagemUrl = "https://upload.wikimedia.org/wikipedia/en/8/8d/Gladiator_ver1.jpg"
            },
        };
    }
}
