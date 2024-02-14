using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("CategoriaServico")]
    public class CategoriaServico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da Categoria de Serviço deve ser informado!")]
        [Display(Name = "Nome da Categoria de Serviço")]
        [MinLength(5, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da Categoria de Serviço deve ser informada!")]
        [Display(Name = "Descrição da Categoria de Serviço")]
        [MinLength(20, ErrorMessage = "Descrição deve possuir no mínimo {1} caracteres!")]
        [MaxLength(200, ErrorMessage = "Descrição deve possuir no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        public List<PrestadorServico> PrestadoresServico { get; set; }
    }
}
