using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio.ModuloEmpresa
{
    public class Empresa : EntidadeBase
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email {  get; set; }
        public string NomeDaEmpresa { get; set; }

        public Empresa(){}

        public Empresa(string login, string senha, string email, string nomeDaEmpresa)
        {
            Login = login;
            Senha = senha;
            Email = email;
            NomeDaEmpresa = nomeDaEmpresa;
        }

        public override void Atualizar(EntidadeBase registro)
        {
            Empresa u = (Empresa)registro;

            Senha = u.Senha;
            NomeDaEmpresa = u.NomeDaEmpresa;
        }

        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
