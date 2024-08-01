using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloPessoa;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloPessoa;

public class RepositorioPessoa : RepositorioBase<Pessoa>, IRepositorioPessoa
{
    public RepositorioPessoa(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Pessoa> ObterRegistros()
    {
        return _dbContext.Pessoas;
    }

    public bool Excluir(Pessoa registro)
    {
        var pessoaSelecionada = _dbContext.Pessoas.FirstOrDefault(p => p.Id == registro.Id);

        if (pessoaSelecionada == null)
            return false;

        _dbContext.Pessoas.Remove(pessoaSelecionada);

        _dbContext.SaveChanges();

        return true;
    }

    public Pessoa SelecionarId(int id)
    {
        return _dbContext.Pessoas.Find(id)!;
    }

    public List<Pessoa> SelecionarTodos()
    {
        return _dbContext.Pessoas.ToList();
    }
}
