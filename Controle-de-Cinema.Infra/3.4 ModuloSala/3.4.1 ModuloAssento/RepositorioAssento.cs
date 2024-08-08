using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSessao;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloSala;

public class RepositorioAssento : RepositorioBase<Assento>, IRepositorioAssento
{
    CinemaDbContext db;

    public RepositorioAssento(CinemaDbContext dbContext) : base(dbContext)
    { db = dbContext; }
    public override Assento SelecionarId(int id)
    {
        return db.Assentos.FirstOrDefault(x => x.Id == id)!;
    }

    public override List<Assento> SelecionarTodos()
    {
        return db.Assentos.ToList();
    }

    protected override DbSet<Assento> ObterRegistros()
    {
        return db.Assentos;
    }
}
