using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSala;
using Controle_de_Cinema.Infra.Compartilhado;

namespace Controle_de_Cinema.Infra.ModuloSala;

public class RepositorioSala : IRepositorioSala
{
    CinemaDbContext _dbContext;
    public RepositorioSala(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Cadastrar(Sala registro)
    {
        _dbContext.Salas.Add(registro);

        _dbContext.SaveChanges();
    }

    public bool Editar(Sala registroOriginal, Sala registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.Atualizar(registroAtualizado);

        _dbContext.Salas.Update(registroOriginal);

        _dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Sala registro)
    {
        if (registro == null)
            return false;

        _dbContext.Salas.Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public Sala SelecionarId(int id)
    {
        return _dbContext.Salas.Find(id)!;
    }

    public List<Sala> SelecionarTodos()
    {
        return _dbContext.Salas.ToList();
    }
}
