using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Sessao : EntidadeBase
{
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public List<Ingresso> ingressos {  get; set; }
    public DateTime InicioDaSessao { get; set; }
    public DateTime FimDaSessao { get; set; }
    public int QuantiaDeIngressos => Sala.Capacidade;


    public Sessao() { }
    public Sessao(Filme filme, Sala sala, DateTime inicio, DateTime fim)
    {
        Filme = filme;
        Sala = sala;
        InicioDaSessao = inicio;
        FimDaSessao = fim;
    }

    public bool CalcularTempoDeSessao()
    {
        return InicioDaSessao.AddMinutes(Filme.Duracao.TotalMinutes) < FimDaSessao;
    }

    #region Overrides
    public override void Atualizar(EntidadeBase registro)
    {
        Sessao update = (Sessao)registro;

        Filme = update.Filme;
        Sala = update.Sala;
        InicioDaSessao = update.InicioDaSessao;
        FimDaSessao = update.FimDaSessao;
    }

    public override void Validar()
    {
        List<string> erros = new List<string>();

        if (Filme == null)
            erros.Add("O campo \"Filme\" deve ser preenchido.");

        if (Sala == null)
            erros.Add("O campo \"Sala\" deve ser preenchido.");

        if (InicioDaSessao == null)
            erros.Add("O campo \"Horário de Inicio\" deve ser preenchido.");

        if (FimDaSessao == null)
            erros.Add("O campo \"Horário de Termino\" deve ser preenchido.");

        if (!CalcularTempoDeSessao())
            erros.Add("A sessão deve ser mais longa que a duração do filme.");
    }

    #endregion
}