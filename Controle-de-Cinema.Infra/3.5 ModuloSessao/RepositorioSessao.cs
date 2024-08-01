using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSessao;
using Controle_de_Cinema.Infra.Compartilhado;

namespace Controle_de_Cinema.Infra.ModuloSessao;

public class RepositorioSessao : IRepositorioSessao
{
    CinemaDbContext _dbContext;

    public RepositorioSessao(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Cadastrar(Sessao registro)
    {
        _dbContext.Sessoes.Add(registro);

        _dbContext.SaveChanges();
    }

    public bool Editar(Sessao registroOriginal, Sessao registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.Atualizar(registroAtualizado);

        _dbContext.Sessoes.Update(registroOriginal);

        _dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Sessao registro)
    {
        if (registro == null)
            return false;
        try{

        _dbContext.Sessoes.Remove(registro);

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
        return _dbContext.Sessoes.Find(id)!;
    }

    public List<Sessao> SelecionarTodos()
    {
        return _dbContext.Sessoes.ToList();
    }
}
