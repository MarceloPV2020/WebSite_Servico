using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da Cidade deve ser informado!")]
        [Display(Name = "Nome da Cidade")]
        [MinLength(2, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A UF deve ser informada!")]
        [Display(Name = "UF")]
        [MaxLength(2, ErrorMessage = "UF deve possuir no máximo {1} caracteres!")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "Código do IBGE deve ser informado!")]
        [Display(Name = "Código do IBGE")]
        [Range(1, 9999999, ErrorMessage = "Código do IBGE deve estar entre 1 e 9999999")]
        public int CodigoIbge { get; set; }
    }
}
