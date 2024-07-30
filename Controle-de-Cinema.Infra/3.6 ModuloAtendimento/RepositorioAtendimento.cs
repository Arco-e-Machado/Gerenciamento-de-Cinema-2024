using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloAtendimento;
using Controle_de_Cinema.Infra.Compartilhado;

namespace Controle_de_Cinema.Infra.ModuloAtendimento;

public class RepositorioAtendimento : IRepositorioAtendimento
{
    CinemaDbContext _dbContext;

    public RepositorioAtendimento(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Cadastrar(Atendimento registro)
    {
        _dbContext.Atendimentos.Add(registro);

        _dbContext.SaveChanges();
    }

    public bool Editar(Atendimento registroOriginal, Atendimento registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.Atualizar(registroAtualizado);

        _dbContext.Atendimentos.Update(registroOriginal);

        _dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Atendimento registro)
    {
        if (registro == null)
            return false;

        _dbContext.Atendimentos.Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public Atendimento SelecionarId(int id)
    {
        return _dbContext.Atendimentos.Find(id)!;
    }

    public List<Atendimento> SelecionarTodos()
    {
        return _dbContext.Atendimentos.ToList();
    }
}
