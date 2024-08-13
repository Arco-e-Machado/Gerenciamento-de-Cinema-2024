using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Atendimento : EntidadeBase
{
    //Acredito que não será necessário a implementação!
    public Pessoa Funcionario { get; set; }

    public Ingresso Ingresso { get; set; }

    public Sessao Sessao { get; set; }

    public string Cliente { get; set; }

    public Atendimento() { }
    public Atendimento(Pessoa funcionario, Ingresso ingresso, Sessao sessao, string cliente)
    {
        Funcionario = funcionario;
        Ingresso = ingresso;
        Sessao = sessao;
        Cliente = cliente;
    }
}