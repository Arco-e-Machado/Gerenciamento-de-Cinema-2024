using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.WebApp.Extensions;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controle_de_Cinema.WebApp.Controllers;

public class SalaController : Controller
{
    readonly private IRepositorioBase<Assento> repositorioAssento;
    readonly private IRepositorioBase<Sala> repositorioSala;
    public SalaController(IRepositorioBase<Sala> repositorioSala, IRepositorioBase<Assento> repositorioAssento)
    {

        this.repositorioSala = repositorioSala;
        this.repositorioAssento = repositorioAssento;
    }
    public IActionResult listar()
    {
        var salas = repositorioSala.SelecionarTodos();

        var ListarSalasVM = salas.Select(s =>
        {
            return new ListarSalaViewModel
            {
                Id = s.Id,
                Numero = s.NumeroDaSala,
                Capacidade = s.Capacidade,
                Assentos = s.Assentos,
            };
        });

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        return View(ListarSalasVM);
    }

    public IActionResult inserir()
    {
        return View();
    }

    [HttpPost]
    public IActionResult inserir(InserirSalaViewModel novaSalaVM)
    {
        var novaSala = new Sala(novaSalaVM.Numero,
                                                    novaSalaVM.Capacidade
                                                    );

        repositorioSala.Cadastrar(novaSala);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{novaSala.Id}] foi inserido com sucesso!"
        });

        return RedirectToAction(nameof(listar));
    }
    public IActionResult editar(int id)
    {
        var salaSelecionada = repositorioSala.SelecionarId(id);

        var editarSalaVM = new EditarSalaViewModel
        {
            Id = salaSelecionada.Id,
            Numero = salaSelecionada.NumeroDaSala,
            Capacidade = salaSelecionada.Capacidade,
        };

        return View(editarSalaVM);
    }

    [HttpPost]
    public IActionResult editar(EditarSalaViewModel editarSalaVM)
    {
        var sala = repositorioSala.SelecionarId(editarSalaVM.Id);

        sala.NumeroDaSala = editarSalaVM.Numero;
        sala.Capacidade = editarSalaVM.Capacidade;

        repositorioSala.Editar(sala);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{sala.Id}] foi editado com sucesso!"
        });

        return RedirectToAction(nameof(listar));

    }

    public IActionResult Excluir(int id)
    {
        var salaSelecionada = repositorioSala.SelecionarId(id);

        var excluirSalaVM = new ExcluirSalaViewModel
        {
            Id = salaSelecionada.Id,
            Numero = salaSelecionada.NumeroDaSala,
            Capacidade = salaSelecionada.Capacidade,
        };

        return View(excluirSalaVM);
    }

    [HttpPost, ActionName("excluir")]
    public IActionResult ExcluirConfirmado(ExcluirSalaViewModel excluirSalaVM)
    {
        var sala = repositorioSala.SelecionarId(excluirSalaVM.Id);

        repositorioSala.Excluir(sala);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{sala.Id}] foi excluído com sucesso!",
        });

        return RedirectToAction(nameof(listar));
    }

    public IActionResult Detalhes(int id)
    {
        var sala = repositorioSala.SelecionarId(id);

        if(sala.Assentos.Count == 0){
            sala.AlocarAssentos(sala.Capacidade);
            repositorioSala.Editar(sala);
        }
        var detalharSalaVM = new DetalharSalaViewModel()
        {
            Id = id,
            Numero = sala.NumeroDaSala,
            Capacidade = sala.Capacidade,
            Assentos = sala.Assentos,

        };


        return View(detalharSalaVM);
    }

    private IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
    {
        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Erro",
            Mensagem = $"Não foi possível encontrar o registro ID [{idRegistro}]!"
        });

        return RedirectToAction(nameof(listar));
    }
}