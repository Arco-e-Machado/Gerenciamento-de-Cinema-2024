using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.Infra.Compartilhado;

namespace Controle_de_Cinema.Infra.ModuloFilme;

public class RepositorioFilme : IRepositorioFilme
{
    CinemaDbContext _dbContext;

    public RepositorioFilme(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Cadastrar(Filme registro)
    {
        _dbContext.Filmes.Add(registro);

        _dbContext.SaveChanges();
    }

    public bool Editar(Filme registroOriginal, Filme registroAtualizado)
    {
        if(registroOriginal == null || registroAtualizado == null)
                return false;

        registroOriginal.Atualizar(registroAtualizado);

        _dbContext.Filmes.Update(registroOriginal);

        _dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Filme registro)
    {
        if (registro == null)
            return false;

        _dbContext.Filmes.Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public Filme SelecionarId(int id)
    {
        return _dbContext.Filmes.Find(id)!;
    }

    public List<Filme> SelecionarTodos()
    {
        return _dbContext.Filmes.ToList();
    }
}
