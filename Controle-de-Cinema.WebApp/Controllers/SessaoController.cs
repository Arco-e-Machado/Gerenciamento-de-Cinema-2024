using Microsoft.AspNetCore.Mvc;
using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controle_de_Cinema.WebApp.Models;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.WebApp.Extensions;
using Controle_de_Cinema.Dominio.ModuloSessao;

namespace Controle_de_Cinema.WebApp.Controllers;

public class SessaoController : Controller
{
    readonly private IRepositorioSessao repositorioSessao;
    readonly private IRepositorioBase<Filme> repositorioFilme;
    readonly private IRepositorioBase<Assento> repositorioAssento;
    readonly private IRepositorioBase<Sala> repositorioSala;
    readonly private IRepositorioBase<Ingresso> repositorioIngresso;

    public SessaoController(
        IRepositorioBase<Sala> repositorioSala,
        IRepositorioBase<Filme> repositorioFilme,
        IRepositorioSessao repositorioSessao,
        IRepositorioBase<Assento> repositorioAssento,
        IRepositorioBase<Ingresso> repositorioIngresso)
    {
        this.repositorioSala = repositorioSala;
        this.repositorioFilme = repositorioFilme;
        this.repositorioSessao = repositorioSessao;
        this.repositorioAssento = repositorioAssento;
        this.repositorioIngresso = repositorioIngresso;
    }

    public IActionResult listar()
    {
        var sessoes = repositorioSessao.SelecionarTodos();

        var agrupamento = repositorioSessao.ObterSessoesAgrupadasPorFilme();

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

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        return View(listarSessoesVM);
    }

    public IActionResult inserir()
    {
        var filmes = repositorioFilme.SelecionarTodos().Select(f => new SelectListItem(f.Nome, f.Id.ToString()));
        var salas = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        var assentos = repositorioSala.SelecionarTodos().Select(s => new SelectListItem(s.Assentos.ToString(), s.Id.ToString()));

        var criarSessao = MapearInformacoes(salas, filmes, assentos);

        return View(criarSessao);
    }

  
    [HttpPost]
    public IActionResult inserir(InserirSessaoViewModel novaSessaoVM)
    {
        #region salas
        var salas = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        #endregion

        #region filmes
        var filmes = repositorioFilme
            .SelecionarTodos()
            .Select(f =>
            new SelectListItem(f.Nome, f.Id.ToString())).ToList();
        #endregion

        #region assentos
        var assentos = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.Assentos.ToString(),
            s.Id.ToString()));
        #endregion

        var sala = repositorioSala.SelecionarId(novaSessaoVM.IdSala.GetValueOrDefault());

        var filme = repositorioFilme.SelecionarId(novaSessaoVM.IdFilme.GetValueOrDefault());

        if (sala.Assentos.Count == 0)
        {
            sala.AlocarAssentos(sala.Capacidade);
            repositorioSala.Editar(sala);
        }

        var novaSessao = new Sessao
        {
            Filme = filme,
            Sala = sala,
            InicioDaSessao = novaSessaoVM.InicioSessao,

        };

        novaSessao.FimDaSessao = novaSessao.CalcularTempoDeSessao(novaSessao.Filme);

        novaSessaoVM.FimSessaoCalculado = novaSessao.FimDaSessao;

        repositorioSessao.Cadastrar(novaSessao);

        var Assentos = sala.Assentos;

        foreach (var Assento in Assentos)
        {
            var ingresso = new Ingresso
            {
                Assento = Assento,
                Sessao = novaSessao,
                Status = true,
                Tipo = false // inicia como inteira
            };

            repositorioIngresso.Cadastrar(ingresso);

            novaSessao.Ingressos.Add(ingresso);
        }

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{novaSessao.Id}] foi cadastrada com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult editar(int id)
    {
        #region salas
        var salas = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        #endregion

        #region filmes
        var filmes = repositorioFilme
            .SelecionarTodos()
            .Select(f =>
            new SelectListItem(f.Nome, f.Id.ToString()));
        #endregion

        #region assentos
        var assentos = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.Assentos.ToString(),
            s.Id.ToString()));
        #endregion

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
    public IActionResult editar(EditarSessaoViewModel editarSessaoVM)
    {
        #region salas
        var salas = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.NumeroDaSala, s.Id.ToString()));
        #endregion

        #region filmes
        var filmes = repositorioFilme
            .SelecionarTodos()
            .Select(f =>
            new SelectListItem(f.Nome, f.Id.ToString()));
        #endregion

        #region assentos
        var assentos = repositorioSala
            .SelecionarTodos()
            .Select(s =>
            new SelectListItem(s.Assentos.ToString(),
            s.Id.ToString()));
        #endregion

        Sessao sessao = MapearSessaoVm(editarSessaoVM);

        repositorioSessao.Editar(sessao);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{sessao.Id}] foi editada com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult excluir(int id)
    {
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
    public IActionResult excluirConfirmado(ExcluirSessaoViewModel excluirSessaoVM)
    {
        var sessao = repositorioSessao.SelecionarId(excluirSessaoVM.Id);

        repositorioSessao.Excluir(sessao);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{sessao.Id}] foi excluída com sucesso!",
            Controlador = "/sessao",
            Link = "/listar"
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult detalhes(int id)
    {
        var sessao = repositorioSessao.SelecionarId(id);

        var detalharSessaoVM = new DetalharSessaoViewModel()
        {
            Id = sessao.Id,
            Sala = sessao.Sala,
            Filme = sessao.Filme,
            Ingressos = sessao.Ingressos,
            Assentos = sessao.Sala.Assentos,
            FimSessao = sessao.FimDaSessao,
            InicioSessao = sessao.InicioDaSessao,
            Ingresso = sessao.QuantiaDeIngressos
        };

        return View(detalharSessaoVM);
    }
    public ViewResult SelecionarAssento(int id)
    {
        var sessao = repositorioSessao.SelecionarId(id);

        var ingressos = sessao.Ingressos
            .Where(x => x.Status == true)
            .Select(i => new SelectListItem(
                $"Ingresso - {i.Assento.Numero}", i.Id.ToString()));

        var sessaoMapeada = new VendaViewModel
        {
            Id = sessao.Id,
            SessaoVM = sessao,
            IngressosVM = ingressos.ToList()
        };

        return View(sessaoMapeada);
    }

    [HttpPost]
    public IActionResult ConfirmarVenda(int id, VendaViewModel venda)
    {
        var sessao = repositorioSessao.SelecionarId(id);

        VendaViewModel? vendaVM = new VendaViewModel
        {
            Id = sessao.Id,
            SessaoVM = sessao,
            MeiaEntrada = venda.MeiaEntrada
        };

        var ingressoSelecionado = vendaVM.SessaoVM.Ingressos.Find(x => x.Id == venda.IngressoVM.Id);

        ingressoSelecionado!.Vender();
        if (venda.MeiaEntrada == true)
            ingressoSelecionado.Desconto();

        repositorioIngresso.Editar(ingressoSelecionado);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O assento {ingressoSelecionado.Assento.Numero.ToString()} foi alocado com sucesso!",
            Controlador = "/sessao",
            Link = $"/detalhes/{id}"
        });

        return RedirectToAction(nameof(listar));
    }
    public IActionResult ListarSessoesDiarias()
    {
        var hoje = DateTime.Today;
        var sessoes = repositorioSessao.SelecionarTodos()
                                       .Where(s => s.InicioDaSessao.Date == hoje)
                                       .ToList();

        IEnumerable<ListarSessaoViewModel> listarSessoesVM = MapearSessoes(sessoes);

        return View("sessoesdiarias", listarSessoesVM);
    }
    private Sessao MapearSessaoVm(EditarSessaoViewModel editarSessaoVM)
    {
        var sessao = repositorioSessao.SelecionarId(editarSessaoVM.Id);

        var sala = repositorioSala.SelecionarId(editarSessaoVM.IdSala.GetValueOrDefault());

        var filme = repositorioFilme.SelecionarId(editarSessaoVM.IdFilme.GetValueOrDefault());

        sessao.Sala = editarSessaoVM.Sala;
        sessao.Filme = editarSessaoVM.Filme;
        sessao.FimDaSessao = editarSessaoVM.FimSessao;
        sessao.InicioDaSessao = editarSessaoVM.InicioSessao;
        return sessao;
    }
    private static IEnumerable<ListarSessaoViewModel> MapearSessoes(List<Sessao> sessoes)
    {
        return sessoes.Select(s =>
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

}