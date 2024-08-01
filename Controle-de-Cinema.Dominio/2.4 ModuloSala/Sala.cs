using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Sala : EntidadeBase
{
    public string NumeroDaSala { get; set; }

    public int Capacidade { get; set; }

    public List<Assento> Assentos { get; set; }

    public bool Status { get; set; } // True = Livre

    public Sala(string numeroDaSala, int capacidade, bool status)
    {
        NumeroDaSala = numeroDaSala;
        Capacidade = capacidade;
        Status = status;
        Assentos = new List<Assento>();

        AlocarAssentos(capacidade);
    }

    public void AlocarAssentos(int Capacidade)
    {
        int fileiraIndex = 0;
        for (int i = 0; i < Capacidade; i++)
        {
            if (i % 10 == 0 && i != 0)
                fileiraIndex++;

            string Fileira = $"{(char)(65 + (fileiraIndex))}";

            Assento novoAssento = new Assento
            {
                Numero = $"{Fileira}-{i + 1}",
                Status = true
            };

            Assentos.Add(novoAssento);
        }
    }


    #region Overrides
    public override void Atualizar(EntidadeBase registro)
    {
        Sala update = (Sala)registro;

        Capacidade = update.Capacidade;
        Status = update.Status;
    }

    public override string? ToString()
    {
        return $"Sala {NumeroDaSala}";
    }

    public override void Validar()
    {
        List<string> erros = new List<string>();

        if (Capacidade == null || Capacidade < 15)
            erros.Add("A Sala deve conter uma quantia mínima de 15 assentos de capacidade.");
    }
#endregion
}