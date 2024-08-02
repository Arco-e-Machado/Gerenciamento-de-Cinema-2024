using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloSala;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Cinema.Infra.ModuloSala;

public class RepositorioSala : RepositorioBase<Sala>, IRepositorioSala
{
    public RepositorioSala(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Sala> ObterRegistros()
    {
        return _dbContext.Salas;
    }

    public override bool Excluir(Sala registro)
    {
        var salaSelecionada = _dbContext.Salas
            .Include(s => s.Assentos)
            .FirstOrDefault(s => s.Id == registro.Id)!;

        if (salaSelecionada == null)
            return false;

        _dbContext.Remove(salaSelecionada);

        _dbContext.SaveChanges();

        return true;
    }

    public Sala SelecionarId(int id)
    {
        return _dbContext.Salas.FirstOrDefault(s => s.Id == id)!;
    }

    public List<Sala> SelecionarTodos()
    {
        return _dbContext.Salas.ToList();
    }
}
