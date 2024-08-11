using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Controle_de_Cinema.Infra.ModuloEmpresa
{
    public class RepositorioEmpresa
    {

        private readonly ClienteDbContext _clientesContext;

        public RepositorioEmpresa(ClienteDbContext dbContext) 
        {
            _clientesContext = dbContext;
        }

        public object buscar(object id)
        {
            throw new NotImplementedException();
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
    }
}
