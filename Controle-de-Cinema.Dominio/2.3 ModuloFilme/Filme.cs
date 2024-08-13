using Controle_de_Cinema.Dominio.Compartilhado;
using Controle_de_Cinema.Dominio.ModuloFilme;

namespace Controle_de_Cinema.Dominio;
public class Filme : EntidadeBase
{
    public string Nome { get; set; }
    public EnumGeneros Genero { get; set; }
    public TimeSpan Duracao { get; set; }

    private int conversorDeMinutos = 1440; // fui testando
    public string ImagemUrl { get; set; }


    public Filme() { }
    public Filme(string nome, EnumGeneros genero, TimeSpan duracao, string imagemUrl)
    {
        Nome = nome;
        Genero = genero;
        Duracao = duracao / conversorDeMinutos;
        ImagemUrl = imagemUrl;
    }

    public override string? ToString()
    {
        return Nome;
    }

}