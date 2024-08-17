using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();

    }
}
