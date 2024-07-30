using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;
public class Filme : EntidadeBase
{
    public string Nome { get; set; }
    public string Genero { get; set; }
    public TimeSpan Duracao { get; set; }

    public Filme() { }
    public Filme(string nome, string genero, TimeSpan duracao)
    {
        Nome = nome;
        Genero = genero;
        Duracao = duracao;
    }

    public override void Atualizar(EntidadeBase registro)
    {
        Filme update = (Filme)registro;

        Nome = update.Nome;
        Genero = update.Genero;
        Duracao = update.Duracao;
    }

    public override void Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Nome.Trim()))
            erros.Add("O filme deve conter um Nome.");

        if (string.IsNullOrEmpty(Genero.Trim()))
            erros.Add("O filme deve conter uma Categoria.");

        if (Duracao == null || Duracao.Minutes <= 0)
            erros.Add("O campo \"Duração\" deve ser preenchido");


    }
}