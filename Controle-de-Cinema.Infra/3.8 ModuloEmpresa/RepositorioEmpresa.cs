using Microsoft.EntityFrameworkCore;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;

namespace Controle_de_Cinema.Infra.ModuloEmpresa
{
    public class RepositorioEmpresa : IRepositorioEmpresa
    {

        private readonly ClienteDbContext _clientesContext;

        public RepositorioEmpresa(ClienteDbContext dbContext)
        {
            _clientesContext = dbContext;
        }

        public void Cadastrar(Empresa novaEmpresa)
        {
            ObterRegistros().Add(novaEmpresa);

            _clientesContext.SaveChanges();
        }

        protected DbSet<Empresa> ObterRegistros()
        {
            return _clientesContext.Empresas;
        }

        public virtual bool Editar(Empresa registro)
        {
            if (registro == null)
                return false;

            ObterRegistros().Update(registro);

            _clientesContext.SaveChanges();

            return true;
        }

        public Empresa SelecionarId(int id)
        {
            return ObterRegistros()
                .FirstOrDefault(r => r.Id == id)!;
        }

        public List<Empresa> SelecionarTodos()
        {
            return ObterRegistros().ToList();
        }

        public bool Excluir(Empresa registro)
        {
            throw new NotImplementedException();
        }
    }
}
