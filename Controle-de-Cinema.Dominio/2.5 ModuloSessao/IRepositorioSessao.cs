using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio.ModuloSessao;

public interface IRepositorioSessao : IRepositorioBase<Sessao>
{
    public List<IGrouping<string, Sessao>> ObterSessoesAgrupadasPorFilme();
}
