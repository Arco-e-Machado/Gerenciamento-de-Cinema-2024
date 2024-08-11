using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Controle_de_Cinema.Dominio.ModuloEmpresa;

namespace Controle_de_Cinema.Infra.Compartilhado
{
    public class ClienteDbContext : DbContext
    {
       public DbSet<Empresa> Empresas { get; set; }

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

            modelBuilder.ApplyConfiguration(new mapperEmpresas());


        base.OnModelCreating(modelBuilder);
    }
}
}