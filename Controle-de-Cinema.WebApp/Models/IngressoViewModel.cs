using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Controle_de_Cinema.WebApp.Models;

public class IngressoViewModel
{
    public int Id { get; set; }
    public Sala Sala {  get; set; }
    public bool MeiaEntrada { get; set; }
    public Pessoa Pessoa { get; set; }
    public Assento assentoTeste {  get; set; }
    public Sessao Sessao { get; set; }
    public int SalaId { get; set; }
    public IEnumerable<SelectListItem>? Assentos { get; set; }
}
public class AssentoViewModel
{
    public string Numero { get; set; }
    public Sala Sala { get; set; }
    public bool ocupada {  get; set; }
}


