using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Pessoa : EntidadeBase
{
    public string Nome { get; set; }

    public string Cpf { get; set; }

    public Pessoa() { }
    public Pessoa(string nome, string cpf)
    {
        Nome = nome;
        Cpf = cpf;
    }

    #region Overrides
    public override void Atualizar(EntidadeBase registro)
    {
        Pessoa update = (Pessoa)registro;

        Nome = update.Nome;
        Cpf = update.Cpf;
    }

    public override void Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrEmpty(Nome.Trim()) || Nome.Length < 3)
            erros.Add("O campo \"nome\" deve ser preenchido com ao menos 3 caracteres.");

        if (string.IsNullOrEmpty(Cpf.Trim()))
            erros.Add("O campo \"cpf\" deve ser preenchido.");
    }
    #endregion
}