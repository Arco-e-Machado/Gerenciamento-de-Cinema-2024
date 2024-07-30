using Controle_de_Cinema.Dominio.Compartilhado;

namespace Controle_de_Cinema.Dominio;

public class Sala : EntidadeBase
{
    public string NumeroDaSala { get; private set; }

    public int Capacidade { get; set; }

    public List<Assento> Assentos { get; set; }

    public bool Status { get; set; } // True = Livre

    public Sala() { }
    public Sala(string numeroDaSala, int capacidade, bool status, List<Assento> assentos)
    {
        NumeroDaSala = numeroDaSala;
        Capacidade = capacidade;
        Status = status;
        Assentos = assentos;

        AlocarAssentos(capacidade);
    }

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

    public void AlocarAssentos(int Capacidade)
    {
        for (int i = 0; i < Capacidade; i++)
        {
            if (i % 8 == 0 && i != 0)
            {
                int fileiraIndex = (i / 8) % 26;
                string Fileira = $"{(char)(65 + fileiraIndex)}";

                Assento novoAssento = new Assento
                {
                    IdDoAssento = $"{Fileira}-{i}",
                    Status = true
                };
                Assentos.Add(novoAssento);
            }
        }
    }

}