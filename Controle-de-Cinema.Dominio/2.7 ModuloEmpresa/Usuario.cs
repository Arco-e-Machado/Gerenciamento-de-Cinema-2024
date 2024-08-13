using Microsoft.AspNetCore.Identity;

namespace Controle_de_Cinema.Dominio.Compartilhado; 

public class Usuario : IdentityUser<int>
{
    public Usuario()
    {
        EmailConfirmed = true;
    }
}