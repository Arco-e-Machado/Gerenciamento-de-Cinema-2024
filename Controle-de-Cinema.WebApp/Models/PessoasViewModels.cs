using System.ComponentModel.DataAnnotations;

namespace Controle_de_Cinema.WebApp.Models;

public class ListarPessoasViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
}

public class InserirPessoasViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Cpf é obrigatório!")]
    public string Cpf { get; set; }
}

public class EditarPessoasViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Cpf é obrigatório!")]
    public string Cpf { get; set; }
}

public class ExcluirPessoasViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
}

public class DetalharPessoasViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
}

