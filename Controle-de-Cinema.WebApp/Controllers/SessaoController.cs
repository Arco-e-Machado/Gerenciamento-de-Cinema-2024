using Microsoft.AspNetCore.Mvc;
using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controle_de_Cinema.WebApp.Models;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloSessao;

namespace Controle_de_Cinema.WebApp.Controllers;

public class SessaoController : Controller
{
    public ViewResult listar()
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        var sessoes = repositorioSessao.SelecionarTodos();

        var listarSessoesVM = sessoes.Select(s =>
        {
            return new ListarSessaoViewModel
            {
                Id = s.Id,
                Sala = s.Sala,
                Filme = s.Filme,
                FimSessao = s.FimDaSessao,
                InicioSessao = s.InicioDaSessao,
                Ingressos = s.QuantiaDeIngressos
            };
        });

        return View(listarSessoesVM);
    }

    public ViewResult inserir()
    {
        var db = new CinemaDbContext();

        var repositorioSala = new RepositorioSala(db);
        var repositorioFilme = new RepositorioFilme(db);

        var filmes = repositorioFilme.SelecionarTodos().Select(f => new SelectListItem(f.Nome, f.Id.ToString()));
        var salas = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        var assentos = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.Assentos.ToString(), s.Id.ToString()));

        var criarSessao = MapearInformacoes(salas, filmes, assentos);

        return View(criarSessao);
    }

    private static InserirSessaoViewModel MapearInformacoes(
        IEnumerable<SelectListItem> salas,
        IEnumerable<SelectListItem> filmes,
        IEnumerable<SelectListItem> assentos)
    {
        return new InserirSessaoViewModel
        {
            salas = salas.ToList(),
            filmes = filmes.ToList(),
            assentos = assentos.ToList()
        };
    }

    [HttpPost]
    public ViewResult inserir(InserirSessaoViewModel novaSessaoVM)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);
        var repositorioFilme = new RepositorioFilme(db);
        var repositorioSessao = new RepositorioSessao(db);

        var filmes = repositorioFilme.SelecionarTodos().Select(f => new SelectListItem(f.Nome, f.Id.ToString()));
        var salas = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        var assentos = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.Assentos.ToString(), s.Id.ToString()));

        var sala = repositorioSala.SelecionarId(novaSessaoVM.IdSala.GetValueOrDefault());
        var filme = repositorioFilme.SelecionarId(novaSessaoVM.IdFilme.GetValueOrDefault());

        var novaSessao = new Sessao(filme,
                                                             sala,
                                                             novaSessaoVM.InicioSessao,
                                                             novaSessaoVM.FimSessao
                                                             );

        repositorioSessao.Cadastrar(novaSessao);

        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID [{novaSessao.Id}] foi cadastrado com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        };

        HttpContext.Response.StatusCode = 201;

        return View("notificacao", Mensagem);
    }

    public ViewResult editar(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);
        var repositorioFilme = new RepositorioFilme(db);
        var repositorioSessao = new RepositorioSessao(db);

        var filmes = repositorioFilme.SelecionarTodos().Select(f => new SelectListItem(f.Nome, f.Id.ToString()));
        var salas = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        var assentos = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.Assentos.ToString(), s.Id.ToString()));

        var editarSessao = MapearInformacoes(salas, filmes, assentos);

        var sessaoSelecionada = repositorioSessao.SelecionarId(id);

        var editarSessaoVM = new EditarSessaoViewModel
        {
            Id = sessaoSelecionada.Id,
            Sala = sessaoSelecionada.Sala,
            Filme = sessaoSelecionada.Filme,
            FimSessao = sessaoSelecionada.FimDaSessao,
            InicioSessao = sessaoSelecionada.InicioDaSessao
        };

        return View(editarSessaoVM);
    }

    [HttpPost]
    public ViewResult editar(EditarSessaoViewModel editarSessaoVM)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);
        var repositorioFilme = new RepositorioFilme(db);
        var repositorioSessao = new RepositorioSessao(db);

        var filmes = repositorioFilme.SelecionarTodos().Select(f => new SelectListItem(f.Nome, f.Id.ToString()));
        var salas = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        var assentos = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.Assentos.ToString(), s.Id.ToString()));

        var sessao = repositorioSessao.SelecionarId(editarSessaoVM.Id);
        var sala = repositorioSala.SelecionarId(editarSessaoVM.IdSala.GetValueOrDefault());
        var filme = repositorioFilme.SelecionarId(editarSessaoVM.IdFilme.GetValueOrDefault());

        sessao.Sala = editarSessaoVM.Sala;
        sessao.Filme = editarSessaoVM.Filme;
        sessao.FimDaSessao = editarSessaoVM.FimSessao;
        sessao.InicioDaSessao = editarSessaoVM.InicioSessao;

        repositorioSessao.Editar(sessao);

        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {sessao.Id} foi editado com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        };

        return View("notificacao", Mensagem);
    }

    public ViewResult excluir(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        var sessaoSelecionada = repositorioSessao.SelecionarId(id);

        var excluirSessaoVM = new ExcluirSessaoViewModel
        {
            Id = sessaoSelecionada.Id,
            Sala = sessaoSelecionada.Sala,
            Filme = sessaoSelecionada.Filme,
            FimSessao = sessaoSelecionada.FimDaSessao,
            InicioSessao = sessaoSelecionada.InicioDaSessao,
            Ingressos = sessaoSelecionada.QuantiaDeIngressos
        };

        return View(excluirSessaoVM);
    }


    [HttpPost, ActionName("excluir")]
    public ViewResult excluirConfirmado(ExcluirSessaoViewModel excluirSessaoVM)
    {

        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        var sessao = repositorioSessao.SelecionarId(excluirSessaoVM.Id);

        repositorioSessao.Excluir(sessao);

        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {sessao.Id} foi excluído com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        };

        return View("notificacao", Mensagem);
    }

    public ViewResult detalhes(int id)
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);

        var sessao = repositorioSessao.SelecionarId(id);

        var detalharSessaoVM = new DetalharSessaoViewModel()
        {
            Id = sessao.Id,
            Sala = sessao.Sala,
            Filme = sessao.Filme,
            Assentos = sessao.Sala.Assentos,
            FimSessao = sessao.FimDaSessao,
            InicioSessao = sessao.InicioDaSessao,
            Ingresso = sessao.QuantiaDeIngressos
        };

        return View(detalharSessaoVM);
    }
}
