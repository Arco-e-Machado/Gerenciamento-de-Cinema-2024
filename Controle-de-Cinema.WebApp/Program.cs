using Microsoft.AspNetCore.Identity;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.ModuloPessoa;
using Controle_de_Cinema.Infra.ModuloSessao;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Microsoft.AspNetCore.Authentication.Cookies;
using Controle_de_Cinema.Dominio.ModuloSessao;
using Controle_de_Cinema.Dominio.ModuloSala;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.Dominio.ModuloPessoa;

namespace Controle_de_Cinema.WebApp;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<CinemaDbContext>();

        builder.Services.AddScoped<IRepositorioAssento, RepositorioAssento>();
        builder.Services.AddScoped<IRepositorioIngressos, RepositorioIngresso>();
        builder.Services.AddScoped<IRepositorioSala, RepositorioSala>();
        builder.Services.AddScoped<IRepositorioFilme, RepositorioFilme>();
        builder.Services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
        builder.Services.AddScoped<IRepositorioSessao, RepositorioSessao>();

        builder.Services.AddIdentity<Usuario, Empresa>()
            .AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequiredLength = 3;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "AspNetCore.Cookies";
                options.Cookie.Expiration = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;

            });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/usuario/login";
            options.AccessDeniedPath = "/usuario/acessoNegado";
        });


        var app = builder.Build();

        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
