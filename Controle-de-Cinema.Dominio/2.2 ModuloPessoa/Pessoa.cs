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
}