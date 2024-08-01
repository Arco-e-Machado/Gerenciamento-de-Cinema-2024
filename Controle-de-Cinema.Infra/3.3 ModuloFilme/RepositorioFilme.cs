using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloFilme;

public class RepositorioFilme : RepositorioBase<Filme>, IRepositorioFilme
{


    public RepositorioFilme(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Filme> ObterRegistros()
    {
        return _dbContext.Filmes ;
    }

    public override bool Excluir(Filme registro)
    {
        var filmeSelecionado = _dbContext.Filmes.FirstOrDefault(f => f .Id == registro.Id)!;

        if (filmeSelecionado == null)
            return false;

        _dbContext.Remove(filmeSelecionado);

        _dbContext.SaveChanges();

        return true;
    }

    public Filme SelecionarId(int id)
    {
        return _dbContext.Filmes.FirstOrDefault(f => f.Id == id)!;
    }

    public List<Filme> SelecionarTodos()
    {
        return _dbContext.Filmes.ToList();
    }
}