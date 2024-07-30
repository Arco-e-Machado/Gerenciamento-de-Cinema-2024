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
        base.OnModelCreating(modelBuilder);
    }
}