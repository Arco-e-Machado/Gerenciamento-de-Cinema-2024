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

    #region Overrides

    public override void Atualizar(EntidadeBase registro)
    {
        Filme update = (Filme)registro;

        Nome = update.Nome;
        Genero = update.Genero;
        Duracao = update.Duracao;
        ImagemUrl = update.ImagemUrl;
    }

    public override void Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Nome.Trim()))
            erros.Add("O filme deve conter um Nome.");

        if (Genero == null)
            erros.Add("O filme deve conter uma Categoria.");

        if (Duracao == null || Duracao.Minutes <= 0)
            erros.Add("O campo \"Duração\" deve ser preenchido");

    }

    public override string? ToString()
    {
        return Nome;
    }
    #endregion
}