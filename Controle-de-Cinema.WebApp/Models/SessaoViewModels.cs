using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Controle_de_Cinema.WebApp.Models;

public class ListarSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime InicioSessao { get; set; }
    public DateTime FimSessao { get; set; }
    public int Ingressos { get; set; }

}
public class InserirSessaoViewModel
{
    public int Id { get; set; }
    public Sala Sala { get; set; }
    public Filme Filme { get; set; }
    public int? IdSala { get; set; }
    public int? IdFilme { get; set; }
    public IEnumerable<SelectListItem>? filmes { get; set; }
    public IEnumerable<SelectListItem>? salas { get; set; }
    public IEnumerable<SelectListItem>? assentos { get; set; }
    public DateTime InicioSessao { get; set; } = DateTime.Today;
    public DateTime FimSessao { get; set; } = DateTime.Today;
}

public class EditarSessaoViewModel
{
    public int Id { get; set; }
    public Sala Sala { get; set; }
    public Filme Filme { get; set; }
    public int? IdSala { get; set; }
    public int? IdFilme { get; set; }
    public IEnumerable<SelectListItem>? filmes { get; set; }
    public IEnumerable<SelectListItem>? salas { get; set; }
    public IEnumerable<SelectListItem>? assentos { get; set; }
    public DateTime InicioSessao { get; set; } = DateTime.Today;
    public DateTime FimSessao { get; set; } = DateTime.Today;
}

public class ExcluirSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime InicioSessao { get; set; }
    public DateTime FimSessao { get; set; }
    public int Ingressos { get; set; }
}

public class DetalharSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime InicioSessao { get; set; }
    public DateTime FimSessao { get; set; }
    public int Ingresso { get; set; }
    public List<Assento> Assentos { get; set; }


}