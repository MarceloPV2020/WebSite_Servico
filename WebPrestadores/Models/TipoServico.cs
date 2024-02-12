using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("TipoServico")]
    public class TipoServico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Tipo de Serviço deve ser informado!")]
        [Display(Name = "Nome do Tipo de Serviço")]
        [MinLength(5, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do Tipo de Serviço deve ser informada!")]
        [Display(Name = "Descrição do Tipo de Serviço")]
        [MinLength(20, ErrorMessage = "Descrição deve possuir no mínimo {1} caracteres!")]
        [MaxLength(200, ErrorMessage = "Descrição deve possuir no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
