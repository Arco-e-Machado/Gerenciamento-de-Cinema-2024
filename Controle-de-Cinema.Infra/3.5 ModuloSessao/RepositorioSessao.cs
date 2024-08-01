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
        try
        {

            var sessaoSelecionada = _dbContext.Sessoes
                .Include(ss => ss.ingressos)
                .FirstOrDefault(ss => ss.Id == registro.Id)!;

            if (sessaoSelecionada == null)
                return false;

            _dbContext.Remove(sessaoSelecionada);

            _dbContext.SaveChanges();

        }
        catch
        {
            return false;
        }

        return true;
    }

    public Sessao SelecionarId(int id)
    {
        return _dbContext.Sessoes
            .Include(ss => ss.Filme)
            .Include(ss => ss.Sala)
            .Include(ss => ss.ingressos)
            .ToList()
            .FirstOrDefault(ss => ss.Id == id)!;
    }

    public List<Sessao> SelecionarTodos()
    {
        return _dbContext.Sessoes
            .Include(ss => ss.Filme)
            .Include(ss => ss.Sala)
            .Include(ss => ss.ingressos)
            .ToList();
    }

}
