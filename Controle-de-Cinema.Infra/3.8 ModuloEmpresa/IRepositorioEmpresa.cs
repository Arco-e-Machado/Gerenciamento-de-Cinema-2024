using Controle_de_Cinema.Dominio.ModuloEmpresa;

namespace Controle_de_Cinema.Infra.ModuloEmpresa
{
    public interface IRepositorioEmpresa
    {
        void Cadastrar(Empresa novaEmpresa);
        bool Editar(Empresa registro);
        bool Excluir(Empresa registro);
        Empresa SelecionarId(int id);
        List<Empresa> SelecionarTodos();
    }
}