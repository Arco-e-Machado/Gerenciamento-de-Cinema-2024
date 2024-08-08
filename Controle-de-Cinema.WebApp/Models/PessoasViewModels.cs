using System.ComponentModel.DataAnnotations;

namespace Controle_de_Cinema.WebApp.Models
{
    public class ListarPessoasViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class InserirPessoasViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório!")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string Cpf { get; set; }
    }

    public class EditarPessoasViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório!")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string Cpf { get; set; }
    }

    public class ExcluirPessoasViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class DetalharPessoasViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}
