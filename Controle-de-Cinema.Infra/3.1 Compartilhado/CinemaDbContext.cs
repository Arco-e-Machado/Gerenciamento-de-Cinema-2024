using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Controle_de_Cinema.Infra.Compartilhado;

public class CinemaDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString("SqlServer")!;

        optionsBuilder.UseSqlServer(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Sessao>();
        modelBuilder.Ignore<Funcionario>();
        modelBuilder.Ignore<Atendimento>();

        modelBuilder.Entity<Assento>(assentoBuilder =>
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
        });

        modelBuilder.Entity<Sala>(salaBuilder =>
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

            salaBuilder.Property(s => s.Status)
                .IsRequired()
                .HasColumnType("bit");

            salaBuilder.HasMany(s => s.Assentos)
                .WithOne(a => a.Sala)
                .HasForeignKey("Sala_Id");

        });


        modelBuilder.Entity<Filme>(filmeBuilder =>
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

        });

        base.OnModelCreating(modelBuilder);
    }
}