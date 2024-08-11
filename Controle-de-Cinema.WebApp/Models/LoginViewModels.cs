namespace Controle_de_Cinema.WebApp.Models;

public class LoginViewModel
{
    public string Usuario { get; set; }
    public string Senha { get; set; }
}

public class RegistrarViewModel
{
    public string Usuario { get; set; }
    public string Senha { get; set; }
    public string Email {  get; set; }
    public string NomeEmpresa { get; set; }

}
