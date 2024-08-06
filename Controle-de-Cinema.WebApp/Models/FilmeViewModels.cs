using System.ComponentModel.DataAnnotations;
using Controle_de_Cinema.Dominio.ModuloFilme;

namespace Controle_de_Cinema.WebApp.Models;

public class ListarFilmeViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public EnumGeneros Genero { get; set; }
    public TimeSpan Duracao { get; set; }
}

public class InserirFilmeViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do filme é obrigatório!")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O Nome do filme é obrigatório!")]
    public EnumGeneros Genero { get; set; }
    [Required(ErrorMessage = "O Genero do filme é obrigatório!")]
    public TimeSpan Duracao { get; set; }

}

public class EditarFilmeViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do filme é obrigatório!")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório!")]
    public EnumGeneros Genero { get; set; }
    [Required(ErrorMessage = "A duração do filme é obrigatório!")]
    public TimeSpan Duracao { get; set; }
}

public class ExcluirFilmeViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public EnumGeneros Genero { get; set; }
    public TimeSpan Duracao { get; set; }
}
public class DetalharFilmeViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public EnumGeneros Genero { get; set; }
    public TimeSpan Duracao { get; set; }
}