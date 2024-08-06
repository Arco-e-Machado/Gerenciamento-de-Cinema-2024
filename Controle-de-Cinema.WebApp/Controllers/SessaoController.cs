using Microsoft.AspNetCore.Mvc;
using Controle_de_Cinema.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controle_de_Cinema.WebApp.Models;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloSessao;
using Microsoft.EntityFrameworkCore;
using Controle_de_Cinema.Dominio.Compartilhado;
using Microsoft.AspNetCore.Components.Forms;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.Dominio.ModuloSala;

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
        var repositorioIngressos = new RepositorioIngresso(db);

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

        var sala = repositorioSala
            .SelecionarId(novaSessaoVM.IdSala
            .GetValueOrDefault());

        var filme = repositorioFilme
            .SelecionarId(novaSessaoVM.IdFilme
            .GetValueOrDefault());



        var novaSessao = new Sessao
        {
            Filme = filme,
            Sala = sala,
            InicioDaSessao = novaSessaoVM.InicioSessao,

        };

        novaSessao.FimDaSessao = novaSessao.CalcularTempoDeSessao(novaSessao.Filme);

        repositorioSessao.Cadastrar(novaSessao);

        var repo = new RepositorioIngresso(db);

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

            repositorioIngressos.Cadastrar(ingresso);

            novaSessao.Ingressos.Add(ingresso);
        }

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
    public ViewResult editar(EditarSessaoViewModel editarSessaoVM)
    {
        var db = new CinemaDbContext();
        var repositorioSala = new RepositorioSala(db);
        var repositorioFilme = new RepositorioFilme(db);
        var repositorioSessao = new RepositorioSessao(db);

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

        var sessao = repositorioSessao
            .SelecionarId(editarSessaoVM.Id);

        var sala = repositorioSala
            .SelecionarId(editarSessaoVM.IdSala
            .GetValueOrDefault());

        var filme = repositorioFilme
            .SelecionarId(editarSessaoVM.IdFilme
            .GetValueOrDefault());

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

        var Ingressos = repositorioSessao.SelecionarId(id).QuantiaDeIngressos;
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
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);
        var repostirioIngressos = new RepositorioIngresso(db);

        var sessao = repositorioSessao.SelecionarId(id);

        var ingressos = sessao.Ingressos
            .Where(x=>x.Status == true).Select(
            i => new SelectListItem($"Ingresso - {i.Assento.Numero}", i.Id.ToString()));

        var sessaoMapeada = new VendaViewModel
        {
            SessaoVM = sessao,
            IngressosVM = ingressos.ToList()
        };
     

        return View(sessaoMapeada);
    }

    [HttpPost]
    public ViewResult ConfirmarVenda(int id,VendaViewModel venda)
    {
        var db = new CinemaDbContext();
        var repositorioSessao = new RepositorioSessao(db);
        var repositorioIngresso = new RepositorioIngresso(db);

        var sessao = repositorioSessao.SelecionarId(id);

        var vendaViewModel = new VendaViewModel
        {
            Id = sessao.Id,
            SessaoVM = sessao,
            MeiaEntrada = venda.MeiaEntrada
        };


        var ingressoSelecionado = vendaViewModel.SessaoVM.Ingressos.Find(x => x.Id == venda.IngressoVM.Id);

        ingressoSelecionado!.Vender();
        if (venda.MeiaEntrada == true)
            ingressoSelecionado.Desconto();

        repositorioIngresso.Editar(ingressoSelecionado);

        var Mensagem = new MensagemViewModel
        {
            Mensagem = $"O assento {ingressoSelecionado.Assento.Numero.ToString()} foi alocado com sucesso!",
            Controlador = "/sessao",
            Link = $"/detalhes/{id}"
        };


        return View("notificacao", Mensagem);
    }

    public class RepositorioAssento : RepositorioBase<Assento>, IRepositorioAssento
    {
        CinemaDbContext db;

        public RepositorioAssento(CinemaDbContext dbContext) : base(dbContext)
        { db = dbContext; }
        public override Assento SelecionarId(int id)
        {
            return db.Assentos.FirstOrDefault(x => x.Id == id)!;
        }

        public override List<Assento> SelecionarTodos()
        {
            return db.Assentos.ToList();
        }

        protected override DbSet<Assento> ObterRegistros()
        {
            return db.Assentos;
        }
    }
    public interface IRepositorioAssento : IRepositorioBase<Assento>
    {
    }

    public class RepositorioIngresso : RepositorioBase<Ingresso>, IRepositorioIngressos
    {
        CinemaDbContext db;

        public RepositorioIngresso(CinemaDbContext dbContext) : base(dbContext)
        { db = dbContext; }
        public override Ingresso SelecionarId(int id)
        {
            return db.Ingressos.FirstOrDefault(x => x.Id == id)!;
        }

        public override List<Ingresso> SelecionarTodos()
        {
            return db.Ingressos.ToList();
        }

        protected override DbSet<Ingresso> ObterRegistros()
        {
            return db.Ingressos;
        }
    }
    public interface IRepositorioIngressos : IRepositorioBase<Ingresso>
    {
    }
}