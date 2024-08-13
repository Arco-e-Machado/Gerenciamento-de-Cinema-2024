using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Assento : EntidadeBase
{
    public string Numero { get; set; }
    public Sala Sala { get; set; }
    public bool Status { get; set; } // true = Livre

    public Assento() { }
    public Assento(string numero, bool status)
    {
        Numero = numero;
        Status = status;
    }

    public string ToString()
    {
        return $"assento {Numero}";
    }
}