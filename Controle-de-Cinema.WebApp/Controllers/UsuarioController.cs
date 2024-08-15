using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Cinema.WebApp.Controllers;

public class UsuarioController : Controller
{
    readonly UserManager<Usuario> userManager;
    readonly SignInManager<Usuario> signInManager;
    readonly RoleManager<Empresa> roleManager;

    public UsuarioController(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Empresa> roleManager
        )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    public IActionResult registrar()
    {
        return View(new RegistrarViewModel());
    }
    [HttpPost]
    public async Task<IActionResult> registrar(RegistrarViewModel registrar)
    {
        if (!ModelState.IsValid)
            return View(registrar);

        var usuario = new Usuario()
        {
            UserName = registrar.Usuario,
            Email = registrar.Email
        };
        var resultadoCreateUser = await userManager.CreateAsync(usuario, registrar.Senha);

        var resultadoCreateRole = await roleManager.FindByNameAsync(registrar.Tipo);

        if (resultadoCreateRole is null)
        {
            var definicao = new Empresa()
            {
                Name = registrar.Tipo,
                NormalizedName = registrar.Tipo!.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            await roleManager.CreateAsync(definicao);

        }

        await userManager.AddToRoleAsync(usuario, registrar.Tipo!);

        if(resultadoCreateUser.Succeeded)
        {
            await signInManager.SignInAsync(usuario, isPersistent: false);
        return RedirectToAction("index", "Home");
        }
        foreach (var erro in resultadoCreateUser.Errors)
            ModelState.AddModelError(string.Empty, erro.Description);

        return View(registrar);
    }

    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login( LoginViewModel login, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
            return View(login);

        var Login = await signInManager.PasswordSignInAsync(
            login.Usuario!,
            login.Senha!,
            false,
            false
            );

        if(Login.Succeeded)
            return LocalRedirect(returnUrl ?? "/");

        ModelState.AddModelError(string.Empty, "Login Inválido");

        return View(login);
    }

    public IActionResult AcessoNegado()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
