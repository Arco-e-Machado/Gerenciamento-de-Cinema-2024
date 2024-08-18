using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSessao;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloSessao;

public class RepositorioSessao : RepositorioBase<Sessao>, IRepositorioSessao
{
    protected override DbSet<Sessao> ObterRegistros()
    {
        return _dbContext.Sessoes;
    }

    public RepositorioSessao(CinemaDbContext dbContext) : base(dbContext)
    { }

    public bool Excluir(Sessao registro)
    {
        if (registro == null)
            return false;

        _dbContext.Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public Sessao SelecionarId(int id)
    {
        return _dbContext.Sessoes
            .Include(ss => ss.Filme)
            .Include(ss => ss.Sala)
            .ThenInclude(sala => sala.Assentos)
            .Include(ss => ss.Ingressos)
            .FirstOrDefault(ss => ss.Id == id)!;
    }

    public List<Sessao> SelecionarTodos()
    {
        return _dbContext.Sessoes
            .Include(ss => ss.Filme)
            .Include(ss => ss.Sala)
            .Include(ss => ss.Ingressos)
            .ToList();
    }

    public List<IGrouping<string, Sessao>> ObterSessoesAgrupadasPorFilme()
    {
        return _dbContext.Sessoes
            .Where(s => !s.Encerrada)
            .Include(s => s.Filme)
            .Include(s => s.Sala)
                .ThenInclude(a => a.Assentos)
            .Include(s => s.Ingressos)
            .GroupBy(s => s.Filme.Nome)
            .AsNoTracking()
            .ToList();
    }
}

