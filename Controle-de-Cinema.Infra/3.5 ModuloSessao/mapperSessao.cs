using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle_de_Cinema.Infra.Compartilhado
{
    public class mapperSessao : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> sessaoBuilder)
        {

            sessaoBuilder.ToTable("TBSessao");

            sessaoBuilder.Property(ss => ss.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            sessaoBuilder.HasOne(ss => ss.Sala)
            .WithMany()
            .IsRequired()
            .HasForeignKey("Sala_Id")
            .OnDelete(DeleteBehavior.Restrict); //lançar uma try catch no controllerSala

            sessaoBuilder.HasOne(ss => ss.Filme)
            .WithMany()
            .IsRequired()
            .HasForeignKey("Filme_Id")
            .OnDelete(DeleteBehavior.NoAction);

            sessaoBuilder.HasMany(ss => ss.Ingressos)
            .WithOne(i => i.Sessao)
            .IsRequired()
            .HasForeignKey("Sessao_Id")
            .OnDelete(DeleteBehavior.Cascade);

            sessaoBuilder.Property(ss => ss.InicioDaSessao)
            .IsRequired()
            .HasColumnType("datetime2");

            sessaoBuilder.Property(ss => ss.FimDaSessao)
            .IsRequired()
            .HasColumnType("datetime2");

            sessaoBuilder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey("Usuario_Id")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            sessaoBuilder.Ignore(ss => ss.QuantiaDeIngressos);


        }
    }
}