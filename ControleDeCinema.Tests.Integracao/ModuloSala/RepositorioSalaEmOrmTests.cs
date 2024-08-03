using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloSala;

namespace ControleDeCinema.Tests.Integracao.ModuloSala
{
    [TestClass]
    [TestCategory("Testes de Integração de Sala")]
    public class RepositorioSalaEmOrmTests
    {
        private RepositorioSala? repositorioSala;
        private CinemaDbContext? dbContext;

        [TestInitialize] // Executado antes de cada teste
        public void PreSetTeste()
        {
            dbContext = new CinemaDbContext();
            repositorioSala = new RepositorioSala(dbContext);

            dbContext.Salas.RemoveRange(dbContext.Salas);
        }

        [TestMethod]
        public void Deve_Cadastrar_Sala()
        {
            // Arrange
            Sala novaSala = new Sala("Sala", 80, false);

            // Act
            repositorioSala!.Cadastrar(novaSala);

            // Assert
            Sala SalaSelecionada = repositorioSala.SelecionarId(novaSala.Id);

            Assert.AreEqual(novaSala, SalaSelecionada);
        }

        [TestMethod]
        public void Deve_Editar_Sala()
        {
            // Arrange
            Sala salaOriginal = new Sala("Sala", 80, false);

            repositorioSala!.Cadastrar(salaOriginal);

            Sala SalaAtualizado = repositorioSala.SelecionarId(salaOriginal.Id);

            SalaAtualizado.NumeroDaSala = "SalaAtualizado";

            // Act
            repositorioSala.Editar(salaOriginal);

            // Assert 
            Assert.AreEqual(SalaAtualizado.NumeroDaSala, salaOriginal.NumeroDaSala);
        }

        [TestMethod]
        public void Deve_Excluir_Sala()
        {
            Sala salaParaExclusao = new Sala("Sala", 80, false);

            repositorioSala!.Cadastrar(salaParaExclusao);

            // Act
            repositorioSala.Excluir(salaParaExclusao);

            // Assert
            Sala SalaSelecionado = repositorioSala.SelecionarId(salaParaExclusao.Id);

            Assert.IsNull(SalaSelecionado);
        }

        [TestMethod]

        public void Deve_Selecionar_Todos()
        {
            List<Sala> SalasParaInserir =
           [
               new("Sala1", 90, false),
               new("Sala2", 80, false),
               new("Sala3", 70, false),
           ];

            foreach (Sala Sala in SalasParaInserir)
                repositorioSala!.Cadastrar(Sala);

            // Act
            List<Sala> SalasSelecionados = repositorioSala!.SelecionarTodos();

            // Assert
            CollectionAssert.AreEqual(SalasParaInserir, SalasSelecionados);
        }
    }
}
