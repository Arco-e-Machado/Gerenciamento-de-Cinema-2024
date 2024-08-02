using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Controle_de_Cinema.WebApp.Models;


public class ListarFuncionarioViewModel
{
    public string Nome {  get; set; }
    public string PessoaId { get; set; }
    public string Cargo { get; set; }
    public string Fone { get; set; }
    public DateTime contratacaoData { get; set; }
}
public class InserirFuncionarioViewModel
{
    public string Nome { get; set; }
    public string PessoaId { get; set; }
    public string Cargo { get; set; } 
    public string Fone { get; set; }
    public DateTime contratacaoData { get; set; } = DateTime.UtcNow;
}
