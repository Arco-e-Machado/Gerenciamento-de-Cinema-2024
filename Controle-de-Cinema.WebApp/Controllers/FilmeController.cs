using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.WebApp.Extensions;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class FilmeController : Controller
{
    readonly private IRepositorioBase<Filme> repositorioFilme;

    public FilmeController(IRepositorioBase<Filme> repositorioFilme)
    {
        this.repositorioFilme = repositorioFilme;
    }

    public IActionResult listar()
    {

        var filmes = repositorioFilme.SelecionarTodos();

        var ListarFilmesVM = filmes.Select(f =>
        {
            return new ListarFilmeViewModel
            {
                Id = f.Id,
                Nome = f.Nome,
                Genero = f.Genero,
                Duracao = f.Duracao,
                image = f.ImagemUrl
            };
        });

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        return View(ListarFilmesVM);
    }

    public IActionResult inserir()
    {
        return View();
    }

    [HttpPost]
    public IActionResult inserir(InserirFilmeViewModel novoFilmeVM)
    {

        var novoFilme = new Filme(novoFilmeVM.Nome,
                                  novoFilmeVM.Genero,
                                  novoFilmeVM.Duracao,
                                  novoFilmeVM.ImagemUrl
                                  );

        repositorioFilme.Cadastrar(novoFilme);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{novoFilme.Id}] foi inserido com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult editar(int id)
    {
        var filmeSelecinado = repositorioFilme.SelecionarId(id);

        var editarFilmeVM = new EditarFilmeViewModel
        {
            Id = filmeSelecinado.Id,
            Nome = filmeSelecinado.Nome,
            Genero = filmeSelecinado.Genero,
            Duracao = filmeSelecinado.Duracao,
            ImagemUrl = filmeSelecinado.ImagemUrl
        };

        return View(editarFilmeVM);
    }

    [HttpPost]
    public IActionResult editar(EditarFilmeViewModel editarFilmeVM)
    { 
        var filme = repositorioFilme.SelecionarId(editarFilmeVM.Id);

        filme.Nome = editarFilmeVM.Nome;
        filme.Duracao = editarFilmeVM.Duracao;
        filme.Genero = editarFilmeVM.Genero;
        filme.ImagemUrl = editarFilmeVM.ImagemUrl;

        repositorioFilme.Editar(filme);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{filme.Id}] foi editado com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult excluir(int id)
    {
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
    public IActionResult excluirConfirmado(ExcluirFilmeViewModel excluirFilmeVM)
    {
        var filme = repositorioFilme.SelecionarId(excluirFilmeVM.Id);

        repositorioFilme.Excluir(filme);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{filme.Id}] foi excluído com sucesso!",
            Controlador = "/filme",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult detalhes(int id)
    {
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