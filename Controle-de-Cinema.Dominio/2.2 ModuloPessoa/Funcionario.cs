using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }

    public string Cpf { get; set; }

    public Funcionario() { }
    public Funcionario(string nome, string cpf)
    {
        Nome = nome;
        Cpf = cpf;
    }
    public override void Atualizar(EntidadeBase registro)
    {
        Funcionario update = (Funcionario)registro;

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
}