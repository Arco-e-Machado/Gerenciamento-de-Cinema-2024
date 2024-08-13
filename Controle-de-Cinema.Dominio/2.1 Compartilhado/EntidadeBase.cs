namespace Controle_de_Cinema.Dominio.Compartilhado;

public abstract class EntidadeBase
{
    public int Id { get; set; }
    public Usuario usuario{ get; set; }
}
