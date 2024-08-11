using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.Compartilhado
{
    internal class mapperEmpresas : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("TBEmpresa");

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NomeDaEmpresa)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Login)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Senha)
                .IsRequired()
                .HasColumnType("varchar(200)");


        }
    }
}