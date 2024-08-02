using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Controle_de_Cinema.WebApp.Models;

public class IngressoViewModel
{
    public int Id { get; set; }
    public bool MeiaEntrada { get; set; }
    public Pessoa Pessoa { get; set; }
    public Sessao Sessao { get; set; }
    public int IdSala { get; set; }
    public IEnumerable<SelectListItem>? assentos { get; set; }

}


