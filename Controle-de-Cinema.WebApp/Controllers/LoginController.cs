using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloEmpresa;
using Controle_de_Cinema.Infra.Servicos;
using Controle_de_Cinema.WebApp.Extensions;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Collections.Generic;

namespace Controle_de_Cinema.WebApp.Controllers;

public class LoginController : Controller
{
    //readonly SqlConnection _dbConnection;

    //public LoginController(ConexaoBancoDeDados dbConnection)
    //{
    //    _dbConnection = dbConnection.Connection;
    //}

    public ViewResult index()
    {
        return View("login");
    }

    public ViewResult login(LoginViewModel Login)
    {
        if (Login.Usuario == "Leo" && Login.Senha == "123")
        {
            var Mensagem = new MensagemViewModel()
            {
                Mensagem = $"Seja bem vindo ao gerenciamento do Cinemark Simulator 2024!",
                Controlador = "/Login",
                Link = "/gerenciar"
            };

            return View("logando", Mensagem);
        }

        return View();
    }

    [HttpPost]
    public IActionResult Registrar(RegistrarViewModel registrar)
    {
        var db = new ClienteDbContext();
        var repositorio = new RepositorioEmpresa(db);

        var novaEmpresa = new Empresa(
            registrar.Usuario,
            registrar.Senha,
            registrar.Email,
            registrar.NomeEmpresa
            );

        repositorio.Cadastrar(novaEmpresa);

        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = $"O registro ID [{novaEmpresa.Id}] foi inserido com sucesso!",
            Controlador = "/login",
            Link = "/login"
        });

        return RedirectToAction(nameof(login));
    }

    public IActionResult CreateDatabase()
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;";

        string databaseName = "NovoBancoDeDados";

        string createDatabaseCommand = $"CREATE DATABASE {databaseName}";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(createDatabaseCommand, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Trata exceções relacionadas ao SQL
                ViewBag.Message = $"Erro ao criar o banco de dados: {ex.Message}";
                return View();
            }
        }

        ViewBag.Message = "Banco de dados criado com sucesso!";
        return View();
    }





public ViewResult contratar()
    {
        return View();
    }
    public ViewResult demitir()
    {
        return View();
    }
    public ViewResult gerenciar(LoginViewModel logado)
    {

        return View();
    }

    [HttpPost]
    public ViewResult inserir(InserirFuncionarioViewModel novoFuncionarioVM)
    {

        return View();
    }
}
