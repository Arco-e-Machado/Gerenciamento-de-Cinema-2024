using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Ingresso : EntidadeBase
{
    public Sessao Sessao { get; set; }

    public Assento Assento { get; set; }

    public decimal Valor { get; set; } = 50;

    public bool Status { get; set; } // true = Livre

    public bool Tipo { get; set; } // false = inteira

    public Ingresso() { }

    public Ingresso(Sessao sessao, Assento assento, bool tipo, bool status)
    {

        Sessao = sessao;
        Assento = assento;
        Tipo = tipo;
        Status = status;
    }
    public void Vender()
    {
        Status = false;
    }

    public void Desconto()
    {
        Tipo = true;
    }
    public override string ToString()
    {
        return $"ingresso {Assento.Numero}";
    }
}
