using Controle_de_Cinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.WebApp.Controllers;
using Controle_de_Cinema.Infra.ModuloPessoa;
using Controle_de_Cinema.Infra.ModuloSessao;
using Controle_de_Cinema.Infra.ModuloEmpresa;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Controle_de_Cinema.WebApp;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<ClienteDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<CinemaDbContext>();
        builder.Services.AddScoped<LoginController>();

        builder.Services.AddScoped<RepositorioEmpresa>();
        builder.Services.AddScoped<IRepositorioBase<Assento>, RepositorioAssento>();
        builder.Services.AddScoped<IRepositorioBase<Ingresso>, RepositorioIngresso>();
        builder.Services.AddScoped<IRepositorioBase<Sala>, RepositorioSala>();
        builder.Services.AddScoped<IRepositorioBase<Filme>, RepositorioFilme>();
        builder.Services.AddScoped<IRepositorioBase<Pessoa>, RepositorioPessoa>();
        builder.Services.AddScoped<IRepositorioBase<Sessao>, RepositorioSessao>();

        builder.Services.AddIdentity<Empresa, Usuario>()
            .AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            
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
