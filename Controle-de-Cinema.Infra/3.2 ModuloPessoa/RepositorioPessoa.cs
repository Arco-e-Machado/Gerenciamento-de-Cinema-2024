using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloPessoa;
using Controle_de_Cinema.Infra.Compartilhado;

namespace Controle_de_Cinema.Infra.ModuloPessoa;

public class RepositorioPessoa : IRepositorioFuncionario
{
    CinemaDbContext _dbContext;

    public RepositorioPessoa(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Cadastrar(Funcionario registro)
    {
        _dbContext.Funcionarios.Add(registro);

        _dbContext.SaveChanges();
    }

    public bool Editar(Funcionario registroOriginal, Funcionario registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.Atualizar(registroAtualizado);

        _dbContext.Funcionarios.Update(registroOriginal);

        _dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Funcionario registro)
    {
        if (registro == null)
            return false;

        _dbContext.Funcionarios.Remove(registro);

        _dbContext.SaveChanges();

        return true;
    }

    public Funcionario SelecionarId(int id)
    {
        return _dbContext.Funcionarios.Find(id)!;
    }

    public List<Funcionario> SelecionarTodos()
    {
        return _dbContext.Funcionarios.ToList();
    }
}
