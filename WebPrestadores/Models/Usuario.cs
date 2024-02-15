using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Usuário deve ser informado!")]
        [Display(Name = "Nome do Usuário")]
        [MinLength(5, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Display(Name = "Prestador")]
        public bool Prestador { get; set; }

        [Display(Name = "Contabilidade")]
        public bool Contabilidade { get; set; }

        [Display(Name = "ID")]
        public string AspNetUsersId { get; set; }

        [Required(ErrorMessage = "O Endereço - Descrição deve ser informado!")]
        [Display(Name = "Endereço - Descrição")]
        [MaxLength(100, ErrorMessage = "Endereço - Descrição deve possuir no máximo {1} caracteres!")]
        public string EnderecoDescricao { get; set; }

        [Required(ErrorMessage = "O Endereço - Número deve ser informado!")]
        [Display(Name = "Endereço - Número")]
        [MaxLength(10, ErrorMessage = "Endereço - Número deve possuir no máximo {1} caracteres!")]
        public string EnderecoNumero { get; set; }

        [Required(ErrorMessage = "O Endereço - Bairro deve ser informado!")]
        [Display(Name = "Endereço - Bairro")]
        [MaxLength(50, ErrorMessage = "Endereço - Bairro deve possuir no máximo {1} caracteres!")]
        public string EnderecoBairro { get; set; }

        [Required(ErrorMessage = "O Endereço - CEP deve ser informado!")]
        [Display(Name = "Endereço - CEP")]
        [MaxLength(9, ErrorMessage = "Endereço - CEP deve possuir no máximo {1} caracteres!")]
        public string EnderecoCep { get; set; }

        [Required(ErrorMessage = "O Endereço - Cidade deve ser informado!")]
        [Display(Name = "Endereço - Cidade")]
        [MaxLength(100, ErrorMessage = "Endereço - Cidade deve possuir no máximo {1} caracteres!")]
        public string EnderecoCidade { get; set; }

        [Required(ErrorMessage = "O Endereço - UF deve ser informado!")]
        [Display(Name = "Endereço - UF")]
        [MaxLength(2, ErrorMessage = "Endereço - UF deve possuir no máximo {1} caracteres!")]
        public string EnderecoUf { get; set; }
    }
}
