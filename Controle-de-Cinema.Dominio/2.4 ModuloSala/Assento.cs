using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Assento : EntidadeBase
{

    public string IdDoAssento { get; set; }


    public bool Status { get; set; } // true = Livre

    public Assento() { }
    public Assento(string idDoAssento, bool status)
    {
        IdDoAssento = idDoAssento;
        Status = status;
    }

    public override void Atualizar(EntidadeBase registro)
    {
        throw new NotImplementedException();
    }

    public override void Validar()
    {
        throw new NotImplementedException();
    }

}