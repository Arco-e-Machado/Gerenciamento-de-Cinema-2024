using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloSessao;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloFilme;
using Controle_de_Cinema.Infra.ModuloPessoa;
using Controle_de_Cinema.Infra.ModuloSala;
using Controle_de_Cinema.Infra.ModuloSessao;

namespace Controle_de_Cinema.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<CinemaDbContext>();

        builder.Services.AddScoped<IRepositorioBase<Assento>, RepositorioAssento>();
        builder.Services.AddScoped<IRepositorioBase<Ingresso>, RepositorioIngresso>();
        builder.Services.AddScoped<IRepositorioBase<Sala>, RepositorioSala>();
        builder.Services.AddScoped<IRepositorioBase<Filme>, RepositorioFilme>();
        builder.Services.AddScoped<IRepositorioBase<Pessoa>, RepositorioPessoa>();
        builder.Services.AddScoped<IRepositorioBase<Sessao>, RepositorioSessao>();


        var app = builder.Build();

        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
