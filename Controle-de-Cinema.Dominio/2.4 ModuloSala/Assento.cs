using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Assento
{
    public int Id { get; set; }
    public string Numero{ get; set; }
    public Sala Sala { get; set; }
    public Ingresso Ingresso { get; set; }
    public bool Status { get; set; } // true = Livre

    public Assento() { }
    public Assento(int id, string numero, bool status)
    {
        Id = id;
        Numero = numero;
        Status = status;
    }

}