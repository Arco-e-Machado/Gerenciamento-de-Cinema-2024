using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Sessao : EntidadeBase
{
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public bool Encerrada { get; set; }
    public List<Ingresso> Ingressos { get; set; }
    public List<Assento> Assentos { get; set; }
    public DateTime InicioDaSessao { get; set; }
    public DateTime FimDaSessao { get; set; }
    public int QuantiaDeIngressos => Sala.Capacidade;


    public Sessao()
    {
        Assentos = new();
        Ingressos = new();
    }
    public Sessao(Filme filme, Sala sala, DateTime inicio, bool encerrada)
    {
        Filme = filme;
        Sala = sala;
        InicioDaSessao = inicio;
        Ingressos = new();
        Assentos = new();

    }

    public DateTime CalcularTempoDeSessao(Filme filme)
    {
        var fimCalculado = InicioDaSessao.Add(filme.Duracao);


        return fimCalculado;
    }

    public void Encerrar(Sessao sessao)
    {
            Encerrada = true;
    }
}