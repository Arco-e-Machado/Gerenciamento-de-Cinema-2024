using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloSessao;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;

namespace Controle_de_Cinema.WebApp.Controllers;

public class IngressoController : Controller
{

    public ViewResult SelecionarAssento(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        var Sessao = repositorioSessao.SelecionarId(id);

        var assentos = Sessao.Sala.Assentos.Select(s => new SelectListItem(s.Numero.ToString(), s.Id.ToString())).ToList();


        var Mapa = MapearInformacoesSessao(assentos, Sessao);

        return View("selecionarassento", Mapa);
    }

    private static IngressoViewModel MapearInformacoesSessao(IEnumerable<SelectListItem> assentos, Sessao sessao)
    {
        return new IngressoViewModel
        {
            SalaId = sessao.Id,
            Assentos = assentos.ToList()
        };
    }

    public ViewResult inserir(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        return View();
    }

}
