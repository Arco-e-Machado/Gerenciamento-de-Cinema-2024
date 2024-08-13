using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Views.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new LoginViewModel
            {
                Saudacao = "Bem-vindo!"
            };
            return View(model);

        }
    }
}
