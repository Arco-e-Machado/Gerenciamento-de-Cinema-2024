using Controle_de_Cinema.Dominio;

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
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime InicioSessao { get; set; }
    public DateTime FimSessao { get; set; }
}
public class EditarSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime InicioSessao { get; set; }
    public DateTime FimSessao { get; set; }
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
    public int Ingressos { get; set; }
}