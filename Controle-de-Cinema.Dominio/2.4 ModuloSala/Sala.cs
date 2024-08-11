using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Sala : EntidadeBase
{
    public string NumeroDaSala { get; set; }

    public int Capacidade { get; set; } //Pequena > 30 :50 < média > 120: grande > 120

    public List<Assento> Assentos { get; set; }

    public Sala()
    {
        Assentos = new List<Assento>();
    }

    public Sala(string numeroDaSala, int capacidade)
    {
        NumeroDaSala = numeroDaSala;
        Capacidade = capacidade;
        Assentos = new List<Assento>();

        AlocarAssentos(capacidade);
    }

    public void AlocarAssentos(int Capacidade)
    {
        if (Assentos.Count != 0)
            return;

        else if (Assentos.Count > Capacidade)
        {
            Assentos.Clear();
            AlocarAssentos(Capacidade);
        }

        int CapacidadeFileira;

        if (Capacidade is > 0 and <= 50)
            CapacidadeFileira = 10;
        else if (Capacidade is > 50 and < 120)
            CapacidadeFileira = 12;
        else
            CapacidadeFileira = 20;

        int fileiraIndex = 0;
        for (int i = 0; i < Capacidade; i++)
        {
            if (i % CapacidadeFileira == 0 && i != 0)
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
        NumeroDaSala = update.NumeroDaSala;
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