using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class FilmeController : Controller
{
    public ViewResult listar()
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filmes = repositorioFilme.SelecionarTodos();

        var ListarFilmesVM = filmes.Select(f =>
        {
            return new ListarFilmeViewModel
            {
                Id = f.Id,
                Nome = f.Nome,
                Genero = f.Genero,
                Duracao = f.Duracao
            };
        });

        return View(ListarFilmesVM);
    }

    public ViewResult inserir()
    {
        return View();
    }

    [HttpPost]
    public ViewResult inserir(InserirFilmeViewModel novoFilmeVM)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var novoFilme = new Filme(novoFilmeVM.Nome,
                                                      novoFilmeVM.Genero,
                                                      novoFilmeVM.Duracao
                                                      );

        repositorioFilme.Cadastrar(novoFilme);

        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID [{novoFilme.Id}] foi cadastrado com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        };
        return View("notificacao", Mensagem);
    }

    public ViewResult editar(int id)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filmeSelecinado = repositorioFilme.SelecionarId(id);

        var editarFilmeVM = new EditarFilmeViewModel
        {
            Id = filmeSelecinado.Id,
            Nome = filmeSelecinado.Nome,
            Genero = filmeSelecinado.Genero,
            Duracao = filmeSelecinado.Duracao
        };

        return View(editarFilmeVM);
    }

    [HttpPost]
    public ViewResult editar(EditarFilmeViewModel editarFilmeVM)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filme = repositorioFilme.SelecionarId(editarFilmeVM.Id);

        filme.Nome = editarFilmeVM.Nome;
        filme.Duracao = editarFilmeVM.Duracao;
        filme.Genero = editarFilmeVM.Genero;

        repositorioFilme.Editar(filme);
        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID [{filme.Id}] foi editar com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        };

        return View("notificacao", Mensagem);
    }

    public ViewResult excluir(int id)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filmeSelecionado = repositorioFilme.SelecionarId(id);

        var excluirFilmeVM = new ExcluirFilmeViewModel
        {
            Id = filmeSelecionado.Id,
            Nome = filmeSelecionado.Nome,
            Duracao = filmeSelecionado.Duracao,
            Genero = filmeSelecionado.Genero
        };

        return View(excluirFilmeVM);
    }

    [HttpPost, ActionName("excluir")]
    public ViewResult excluirConfirmado(ExcluirFilmeViewModel excluirFilmeVM)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filme = repositorioFilme.SelecionarId(excluirFilmeVM.Id);

        repositorioFilme.Excluir(filme);

        var mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {filme.Id} foi excluído com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        };

        return View("notificacao", mensagem);
    }

    public ViewResult detalhes(int id)
    {
        var db = new CinemaDbContext();
        var repositorioFilme = new RepositorioFilme(db);

        var filme = repositorioFilme.SelecionarId(id);

        var detalharFilmeVM = new DetalharFilmeViewModel()
        {
            Id = filme.Id,
            Nome = filme.Nome,
            Duracao = filme.Duracao,
            Genero = filme.Genero
        };

        return View(detalharFilmeVM);
    }

}