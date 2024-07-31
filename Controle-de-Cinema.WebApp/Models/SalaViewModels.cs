using System.ComponentModel.DataAnnotations;

namespace Controle_de_Cinema.WebApp.Models;

public class ListarSalaViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O número da sala é obrigatório!")]
    public string Numero { get; set; }

    [Required(ErrorMessage = "A capacidade da sala é obrigatório!")]
    public int Capacidade { get; set; }
    public string Status { get; set; }
}

public class InserirSalaViewModel
{
    public string Numero { get; set; }
    public int Capacidade { get; set; }
    public bool Status { get; set; }
}

public class EditarSalaViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O número da sala é obrigatório!")]
    public string Numero { get; set; }

    [Required(ErrorMessage = "A capacidade da sala é obrigatório!")]
    public int Capacidade { get; set; }

    public string Status { get; set; }
}

public class ExcluirSalaViewModel
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public int Capacidade { get; set; }
    public string Status { get; set; }
}

public class DetalharSalaViewModel
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public int Capacidade { get; set; }
    public string Status { get; set; }
}