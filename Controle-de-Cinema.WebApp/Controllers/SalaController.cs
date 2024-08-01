using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class SalaController : Controller
{
    public ViewResult listar()
    {
        var db = new CinemaDbContext();
        var reposirotioSala = new RepositorioSala(db);

        var salas = reposirotioSala.SelecionarTodos();

        var ListarSalasVM = salas.Select(s =>
        {
            return new ListarSalaViewModel
            {
                Id = s.Id,
                Numero = s.NumeroDaSala,
                Capacidade = s.Capacidade,
                Assentos = s.Assentos,
                Status = s.Status ? "Ocupada" : "Livre"
            };
        });


        return View(ListarSalasVM);
    }

    public ViewResult inserir()
    {
        return View();
    }

    [HttpPost]
    public ViewResult inserir(InserirSalaViewModel novaSalaVM)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var novaSala = new Sala(novaSalaVM.Numero,
                                                    novaSalaVM.Capacidade,
                                                    novaSalaVM.Status
                                                    );

        repositorioSala.Cadastrar(novaSala);


        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID [{novaSala.Id}] foi cadastrado com sucesso!",
            Controlador = "/sala",
            Link = "/listar"
        };

        HttpContext.Response.StatusCode = 201;

        return View("notificacao", Mensagem);
    }

    public ViewResult editar(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var salaSelecionada = repositorioSala.SelecionarId(id);

        var editarSalaVM = new EditarSalaViewModel
        {
            Id = salaSelecionada.Id,
            Numero = salaSelecionada.NumeroDaSala,
            Capacidade = salaSelecionada.Capacidade,
            Status = salaSelecionada.Status ? "Ocupada" : "Livre"
        };

        return View(editarSalaVM);
    }

    [HttpPost]
    public ViewResult editar(EditarSalaViewModel editarSalaVM)
    {
        //if (!ModelState.IsValid)
        //    return View(editarSalaVM);

        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var sala = repositorioSala.SelecionarId(editarSalaVM.Id);

        sala.NumeroDaSala = editarSalaVM.Numero;
        sala.Capacidade = editarSalaVM.Capacidade;

        repositorioSala.Editar(sala);

        var mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {sala.Id} foi editado com sucesso!",
            Controlador = "/sala",
            Link = "/listar"
        };

        return View("notificacao", mensagem);

    }

    public ViewResult Excluir(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var salaSelecionada = repositorioSala.SelecionarId(id);

        var excluirSalaVM = new ExcluirSalaViewModel
        {
            Id = salaSelecionada.Id,
            Numero = salaSelecionada.NumeroDaSala,
            Capacidade = salaSelecionada.Capacidade,
            Status = salaSelecionada.Status ? "Ocupada" : "Livre"
        };

        return View(excluirSalaVM);
    }

    [HttpPost, ActionName("excluir")]
    public ViewResult ExcluirConfirmado(ExcluirSalaViewModel excluirSalaVM)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var sala = repositorioSala.SelecionarId(excluirSalaVM.Id);

        repositorioSala.Excluir(sala);

        var mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {sala.Id} foi excluído com sucesso!",
            Controlador = "/sala",
            Link = "/listar"
        };

        return View("notificacao", mensagem);
    }

    public ViewResult Detalhes(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);

        var sala = repositorioSala.SelecionarId(id);

        var detalharSalaVM = new DetalharSalaViewModel()
        {
            Id = id,
            Numero = sala.NumeroDaSala,
            Capacidade = sala.Capacidade,
            Assentos = sala.Assentos,
            Status = sala.Status ? "Ocupada" : "Livre"
        };


        return View(detalharSalaVM);
    }
}