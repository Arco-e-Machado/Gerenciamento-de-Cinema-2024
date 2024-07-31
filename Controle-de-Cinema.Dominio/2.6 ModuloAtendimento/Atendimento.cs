using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Atendimento : EntidadeBase
{
    //Acredito que não será necessário a implementação!
    public Funcionario Funcionario { get; set; }

    public Ingresso Ingresso { get; set; }

    public Sessao Sessao { get; set; }

    public string Cliente { get; set; }

    public Atendimento() { }
    public Atendimento(Funcionario funcionario, Ingresso ingresso, Sessao sessao, string cliente)
    {
        Funcionario = funcionario;
        Ingresso = ingresso;
        Sessao = sessao;
        Cliente = cliente;
    }

    #region Overrides
    public override void Atualizar(EntidadeBase registro)
    {
        Atendimento update = (Atendimento)registro;

        Funcionario = update.Funcionario;
        Ingresso = update.Ingresso;
        Sessao = update.Sessao;
        Cliente = update.Cliente;
    }

    public override void Validar()
    {
        throw new NotImplementedException();
    }
    #endregion
}