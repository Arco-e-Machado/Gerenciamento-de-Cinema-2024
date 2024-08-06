using Controle_de_Cinema.Dominio;
using Controle_de_Cinema.Infra.Compartilhado;
using Controle_de_Cinema.Infra.ModuloFilme;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleDeCinema.Tests.Integracao.ModuloFilme
{
    [TestClass]
    [TestCategory("Testes de Integração de Filme")]
    public class RepositorioFilmeEmOrmTests
    {
        private RepositorioFilme? repositorioFilme;
        private CinemaDbContext? dbContext;

        [TestInitialize] // Executado antes de cada teste
        public void PreSetTeste()
        {
            dbContext = new CinemaDbContext();
            repositorioFilme = new RepositorioFilme(dbContext);

            dbContext.Filmes.RemoveRange(dbContext.Filmes);
        }

        [TestMethod]
        public void Deve_Cadastrar_Filme()
        {
            // Arrange
            Filme novoFilme = new Filme(
                "Filme",
                Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Terror,
                new TimeSpan(0, 0, 30));

            // Act
            repositorioFilme!.Cadastrar(novoFilme);

            // Assert
            Filme filmeSelecionado = repositorioFilme.SelecionarId(novoFilme.Id);

            Assert.AreEqual(novoFilme, filmeSelecionado);
        }

        [TestMethod]
        public void Deve_Editar_Filme()
        {
            // Arrange
            Filme filmeOriginal = new Filme(
                "Filme",
                Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Ação,
                new TimeSpan(0, 0, 30));

            repositorioFilme!.Cadastrar(filmeOriginal);

            Filme filmeAtualizado = repositorioFilme.SelecionarId(filmeOriginal.Id);

            filmeAtualizado.Nome = "FilmeAtualizado";

            // Act
            repositorioFilme.Editar(filmeOriginal);

            // Assert 
            Assert.AreEqual(filmeAtualizado.Nome, filmeOriginal.Nome);
        }

        [TestMethod]
        public void Deve_Excluir_Filme()
        {
            Filme filmeParaExclusao = new Filme(
                 "Filme",
                Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Ação,
                new TimeSpan(0, 0, 30));

            repositorioFilme!.Cadastrar(filmeParaExclusao);

            // Act
            repositorioFilme.Excluir(filmeParaExclusao);

            // Assert
            Filme filmeSelecionado = repositorioFilme.SelecionarId(filmeParaExclusao.Id);

            Assert.IsNull(filmeSelecionado);
        }

        [TestMethod]

        public void Deve_Selecionar_Todos()
        {
            List<Filme> filmesParaInserir =
           [
               new("Filme 1",Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Animação, new TimeSpan(0,0,30)),
               new("Filme 2",Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Terror, new TimeSpan(0,0,30)),
               new("Filme 3",Controle_de_Cinema.Dominio.ModuloFilme.EnumGeneros.Ação, new TimeSpan(0,0,30))
           ];

            foreach (Filme filme in filmesParaInserir)
                repositorioFilme!.Cadastrar(filme);

            // Act
            List<Filme> filmesSelecionados = repositorioFilme!.SelecionarTodos();

            // Assert
            CollectionAssert.AreEqual(filmesParaInserir, filmesSelecionados);
        }
    }
}
