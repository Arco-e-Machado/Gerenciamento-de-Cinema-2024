using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class EmpresaController : Controller
{
    readonly UserManager<Usuario> userManager;
    readonly SignInManager<Usuario> signInManager;
    readonly RoleManager<Empresa> roleManager;
public EmpresaController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public IActionResult registrar()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> registrar( RegistrarViewModel registrar)
    {
        var usuario = new Usuario()
        {UserName = registrar.Usuario,
        Email = registrar.Email
        };
        var resultadoCreateUser = await userManager.CreateAsync(usuario, registrar.Senha);

        var resultadoCreateRole = await roleManager.FindByNameAsync(registrar.Tipo);


    }
}
