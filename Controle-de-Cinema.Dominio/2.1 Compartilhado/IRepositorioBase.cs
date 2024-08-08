namespace Controle_de_Cinema.Dominio.Compartilhado;

public interface IRepositorioBase<Generico> where Generico : EntidadeBase
{
    void Cadastrar(Generico registro);
    bool Editar (Generico registroOriginal);
    bool Excluir(Generico registro);
    Generico SelecionarId(int id);
    List<Generico> SelecionarTodos();

}
