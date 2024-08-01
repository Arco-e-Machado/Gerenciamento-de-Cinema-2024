using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloPessoa;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace Controle_de_Cinema.WebApp.Controllers;

public class PessoaController : Controller
{
    public ViewResult listar()
    {
        var db = new CinemaDbContext();
        var repositorioFuncionario = new RepositorioPessoa(db);

        var pessoas = repositorioFuncionario.SelecionarTodos();

        var ListarFuncionarioVM = pessoas.Select(p =>
        {
            return new ListarPessoasViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Cpf = p.Cpf
            };
        });

        return View(ListarFuncionarioVM);
    }

    public ViewResult inserir()
    {
        return View();
    }

    public ViewResult inserir(InserirPessoasViewModel novaPessoaVM)
    {
        var db = new CinemaDbContext();
        var repositorioPessoa = new RepositorioPessoa(db);

        var novaPessoa = new Pessoa(novaPessoaVM.Nome,
                                                             novaPessoaVM.Cpf
                                                             );

        repositorioPessoa.Cadastrar(novaPessoa);

        var Mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID [{novaPessoa.Id}] foi cadastrado com sucesso!",
            Controlador = "/pessoa",
            Link = "/listar"
        };

        HttpContext.Response.StatusCode = 201;

        return View("notificacao", Mensagem);
    }

    public ViewResult editar(int id)
    {
        var db = new CinemaDbContext();
        var repositorioPessoas = new RepositorioPessoa(db);

        var pessoaSelecionada = repositorioPessoas.SelecionarId(id);

        var editarPessoaVM = new EditarPessoasViewModel
        {
            Id = pessoaSelecionada.Id,
            Nome = pessoaSelecionada.Nome,
            Cpf = pessoaSelecionada.Cpf
        };

        return View(editarPessoaVM);
    }

    [HttpPost]
    public ViewResult editar(EditarPessoasViewModel editarPessoaVM)
    {
        if (!ModelState.IsValid)
            return View(editarPessoaVM);

        var db = new CinemaDbContext();
        var repositorioPessoas = new RepositorioPessoa(db);

        var pessoa = repositorioPessoas.SelecionarId(editarPessoaVM.Id);

        pessoa.Nome = editarPessoaVM.Nome;
        pessoa.Cpf = editarPessoaVM.Cpf;

        repositorioPessoas.Editar(pessoa);

        var mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {pessoa.Id} foi editado com sucesso!",
            Controlador = "/pessoa",
            Link = "/listar"
        };

        return View("notificacao", mensagem);
    }

    public ViewResult excluir(int id)
    {
        var db = new CinemaDbContext();
        var repositorioPessoa = new RepositorioPessoa(db);

        var pessoaSelecionada = repositorioPessoa.SelecionarId(id);

        var excluirPessoaVM = new ExcluirPessoasViewModel
        {
            Id = pessoaSelecionada.Id,
            Nome = pessoaSelecionada.Nome,
            Cpf = pessoaSelecionada.Cpf
        };

        return View(excluirPessoaVM);
    }

    [HttpPost, ActionName("excluir")]
    public ViewResult excluirConfirmado(ExcluirPessoasViewModel excluirPessoaVM)
    {
        var db = new CinemaDbContext();
        var repositorioPessoa = new RepositorioPessoa(db);

        var pessoa = repositorioPessoa.SelecionarId(excluirPessoaVM.Id);

        repositorioPessoa.Excluir(pessoa);

        var mensagem = new MensagemViewModel()
        {
            Mensagem = $"O registro com o ID {pessoa.Id} foi editado com sucesso!",
            Controlador = "/pessoa",
            Link = "/listar"
        };

        return View("notificacao", mensagem);
    }

    public ViewResult detalhes(int id)
    {
        var db = new CinemaDbContext();
        var repositorioPessoa = new RepositorioPessoa(db);

        var pessoa = repositorioPessoa.SelecionarId(id);

        var detalharPessoaVM = new DetalharPessoasViewModel()
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf
        };

        return View(detalharPessoaVM);
    }
}
