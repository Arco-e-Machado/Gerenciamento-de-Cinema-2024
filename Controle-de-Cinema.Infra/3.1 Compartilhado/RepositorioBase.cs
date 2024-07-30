using Controle_de_Cinema.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.Compartilhado;

public abstract class RepositorioBase<Generico> where Generico : EntidadeBase
{
    protected readonly CinemaDbContext _dbContext;

    public RepositorioBase(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected abstract DbSet<Generico> ObterRegistros();

    public void Cadastrar(Generico registro)
    {
        ObterRegistros().Add(registro);

        _dbContext.SaveChanges();
    }

    public virtual bool Editar(Generico registro, Generico registroAtualizado)
    {
        if (registro == null || registroAtualizado == null)
            return false;

        registro.Atualizar(registroAtualizado);

        ObterRegistros().Update(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public virtual bool Editar(Generico registro)
    {
        if (registro == null)
            return false;

        ObterRegistros().Update(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public virtual bool Excluir(Generico registro)
    {
        if (registro == null)
            return false;

        ObterRegistros().Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public virtual Generico SelecionarId(int id)
    {
        return ObterRegistros()
            .FirstOrDefault(r => r.Id == id)!;
    }

    public virtual List<Generico> SelecionarTodos()
    {
        return ObterRegistros().ToList();
    }

}
