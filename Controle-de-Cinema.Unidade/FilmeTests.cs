

using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Dominio.ModuloFilme;
using Controle_de_Cinema.WebApp.Models;

namespace Controle_de_Cinema.Unidade
{
    [TestClass]
    public class FilmeTests
    {
        [TestMethod]
        public void Deve_Inserir_Filme_Corretamente()
        {
            // Arrange (Preparação Teste)

            Filme FilmeTeste = new ("", EnumGeneros.Terror,TimeSpan.MinValue);

            List<string> errosEsperados = 
                [
                    
                ];


        }
    }
}