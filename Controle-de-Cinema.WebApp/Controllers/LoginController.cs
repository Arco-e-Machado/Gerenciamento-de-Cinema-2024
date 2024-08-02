using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Controle_de_Cinema.WebApp.Controllers
{
    public class LoginController : Controller
    {
        public ViewResult index()
        {
            return View("gerenciar");
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
}
