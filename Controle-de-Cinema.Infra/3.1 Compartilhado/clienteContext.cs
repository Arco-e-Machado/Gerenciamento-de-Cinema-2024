using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.Infra.Servicos;

namespace Controle_de_Cinema.Infra.Compartilhado
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
    : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Banco = ConexaoBancoDeDados.Instance;

            optionsBuilder.UseSqlServer(Banco.Connection);

            ApplyMigrations();

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new mapperEmpresas());


            base.OnModelCreating(modelBuilder);
        }
    public void ApplyMigrations()
    {
        this.Database.Migrate();
    }


    }
}