using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Ingresso : EntidadeBase
{
    public Sessao Sessao { get; set; }

    public Assento Assento { get; set; }

    public decimal Valor { get; set; }

    public bool Status { get; set; } // true = Livre

    public Ingresso() { }

    public Ingresso(Sessao sessao, Assento assento, decimal valor, bool status)
    {
        Sessao = sessao;
        Assento = assento;
        Valor = valor;
        Status = status;
    }

    #region Overrides
    public override void Atualizar(EntidadeBase registro)
    {
        Ingresso update = (Ingresso)registro;

        Sessao = update.Sessao;
        Assento = update.Assento;
        Valor = update.Valor;
        Status = update.Status;
    }

    public override void Validar()
    {
        throw new NotImplementedException();
    }
    #endregion
}
