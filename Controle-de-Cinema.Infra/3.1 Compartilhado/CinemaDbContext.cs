using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloSessao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Controle_de_Cinema.Infra.Compartilhado;

public class CinemaDbContext : IdentityDbContext<Usuario, Empresa, int>
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Ingresso> Ingressos { get; set; }
    public DbSet<Assento> Assentos { get; set; }

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

        modelBuilder.ApplyConfiguration(new mapperFilme());
        modelBuilder.ApplyConfiguration(new mapperSala());
        modelBuilder.ApplyConfiguration(new mapperAssento());
        modelBuilder.ApplyConfiguration(new mapperSessao());
        modelBuilder.ApplyConfiguration(new mapperIngresso());

        #region modelEntity
        //modelBuilder.Entity<Sala>(salaBuilder =>
        //{
        //    salaBuilder.ToTable("TBSala");

        //    salaBuilder.Property(s => s.Id)
        //        .IsRequired()
        //        .ValueGeneratedOnAdd();

        //    salaBuilder.Property(s => s.NumeroDaSala)
        //        .IsRequired()
        //        .HasColumnType("varchar(100)");

        //    salaBuilder.Property(s => s.Capacidade)
        //        .IsRequired()
        //        .HasColumnType("int");

        //    salaBuilder.HasMany(s => s.Assentos)
        //        .WithOne(a => a.Sala)
        //        .HasForeignKey("Sala_Id")
        //        .OnDelete(DeleteBehavior.Cascade);

        //});
        //modelBuilder.Entity<Filme>(filmeBuilder =>
        //{
        //    filmeBuilder.ToTable("TBfilme");

        //    filmeBuilder.Property(f => f.Id)
        //        .IsRequired()
        //        .ValueGeneratedOnAdd();

        //    filmeBuilder.Property(f => f.Nome)
        //        .IsRequired()
        //        .HasColumnType("varchar(100)");

        //    filmeBuilder.Property(f => f.Duracao)
        //        .IsRequired()
        //        .HasColumnType("time");

        //    filmeBuilder.Property(f => f.Genero)
        //        .IsRequired()
        //        .HasColumnType("int");
        //});
        //modelBuilder.Entity<Pessoa>(pessoaBuilder =>
        //{
        //    pessoaBuilder.ToTable("TBPessoa");

        //    pessoaBuilder.Property(p => p.Id)
        //    .IsRequired()
        //    .ValueGeneratedOnAdd();

        //    pessoaBuilder.Property(p => p.Nome)
        //    .IsRequired()
        //    .HasColumnType("varchar(200)");

        //    pessoaBuilder.Property(p => p.Cpf)
        //    .IsRequired()
        //    .HasColumnType("varchar(20)");

        //});
        //modelBuilder.Entity<Assento>(assentoBuilder =>
        //{
        //    assentoBuilder.ToTable("TBAssento");

        //    assentoBuilder.Property(a => a.Id)
        //        .IsRequired()
        //        .ValueGeneratedOnAdd();

        //    assentoBuilder.Property(a => a.Numero)
        //        .IsRequired()
        //        .HasColumnType("varchar(100)");

        //    assentoBuilder.Property(a => a.Status)
        //        .IsRequired()
        //        .HasColumnType("bit");
        //});
        //modelBuilder.Entity<Sessao>(sessaoBuilder =>
        //{
        //    sessaoBuilder.ToTable("TBSessao");

        //    sessaoBuilder.Property(ss => ss.Id)
        //    .IsRequired()
        //    .ValueGeneratedOnAdd();

        //    sessaoBuilder.HasOne(ss => ss.Sala)
        //    .WithMany()
        //    .IsRequired()
        //    .HasForeignKey("Sala_Id")
        //    .OnDelete(DeleteBehavior.Restrict); //lançar uma try catch no controllerSala

        //    sessaoBuilder.HasOne(ss => ss.Filme)
        //    .WithMany()
        //    .IsRequired()
        //    .HasForeignKey("Filme_Id")
        //    .OnDelete(DeleteBehavior.NoAction);

        //    sessaoBuilder.HasMany(ss => ss.Ingressos)
        //    .WithOne(i => i.Sessao)
        //    .IsRequired()
        //    .HasForeignKey("Sessao_Id")
        //    .OnDelete(DeleteBehavior.Cascade);

        //    sessaoBuilder.Property(ss => ss.InicioDaSessao)
        //    .IsRequired()
        //    .HasColumnType("datetime2");

        //    sessaoBuilder.Property(ss => ss.FimDaSessao)
        //    .IsRequired()
        //    .HasColumnType("datetime2");

        //    sessaoBuilder.Ignore(ss => ss.QuantiaDeIngressos);


        //});
        //modelBuilder.Entity<Ingresso>(ingressoBuilder =>
        //{
        //    ingressoBuilder.ToTable("TBIngresso");

        //    ingressoBuilder.Property(i => i.Id)
        //    .IsRequired()
        //    .ValueGeneratedOnAdd();


        //    ingressoBuilder.Property(i => i.Valor)
        //    .IsRequired()
        //    .HasColumnType("decimal");

        //    ingressoBuilder.Property(i => i.Status)
        //    .IsRequired()
        //    .HasColumnType("bit");

        //    //ingressoBuilder.HasOne(i => i.Sessao)
        //    //.WithMany(ss => ss.Ingressos)
        //    //.IsRequired()
        //    //.HasForeignKey("Sessao_Id")
        //    //.OnDelete(DeleteBehavior.Cascade);
        //});

        #endregion

        base.OnModelCreating(modelBuilder);
    }
}