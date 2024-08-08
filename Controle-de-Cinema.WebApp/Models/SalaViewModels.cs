using Controle_de_Cinema.Dominio; // Certifique-se de que o namespace correto para Assento é usado
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Controle_de_Cinema.WebApp.Models
{
    public class ListarSalaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número da sala é obrigatório!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A capacidade da sala é obrigatória!")]
        public int Capacidade { get; set; }

        public List<Assento> Assentos { get; set; }

        public string Status { get; set; } 
    }

    public class InserirSalaViewModel
    {
        [Required(ErrorMessage = "O número da sala é obrigatório!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A capacidade da sala é obrigatória!")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage = "O status da sala é obrigatório!")]
        public bool Status { get; set; }
    }

    public class EditarSalaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número da sala é obrigatório!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A capacidade da sala é obrigatória!")]
        public int Capacidade { get; set; }

        public string Status { get; set; } 
    }

    public class ExcluirSalaViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int Capacidade { get; set; }
        public string Status { get; set; } 
    }

    public class DetalharSalaViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public List<Assento> Assentos { get; set; }
        public int Capacidade { get; set; }
        public string Status { get; set; }
    }
}
