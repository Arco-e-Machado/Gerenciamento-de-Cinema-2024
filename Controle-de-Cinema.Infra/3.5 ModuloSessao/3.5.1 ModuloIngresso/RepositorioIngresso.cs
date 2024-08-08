using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSala;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloSessao;

public class RepositorioIngresso : RepositorioBase<Ingresso>, IRepositorioIngressos
{
    CinemaDbContext db;

    public RepositorioIngresso(CinemaDbContext dbContext) : base(dbContext)
    { db = dbContext; }
    public override Ingresso SelecionarId(int id)
    {
        return db.Ingressos.FirstOrDefault(x => x.Id == id)!;
    }

    public override List<Ingresso> SelecionarTodos()
    {
        return db.Ingressos.ToList();
    }

    protected override DbSet<Ingresso> ObterRegistros()
    {
        return db.Ingressos;
    }
}
