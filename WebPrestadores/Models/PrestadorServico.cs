using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("PrestadorServico")]
    public class PrestadorServico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Prestador deve ser informado!")]
        [Display(Name = "Nome")]
        [MinLength(5, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(200, ErrorMessage = "Descrição deve possuir no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        public int TipoServicoId { get; set; }
        public virtual TipoServico TipoServico { get; set; }
    }
}
