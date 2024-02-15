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
        [Display(Name = "Nome da Loja/Empresa")]
        [MinLength(5, ErrorMessage = "Nome deve possuir no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "Nome deve possuir no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(200, ErrorMessage = "Descrição deve possuir no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "A cidade de prestação do serviço deve ser informada!")]
        [Display(Name = "Cidade de Prestação")]
        [MaxLength(100, ErrorMessage = "Cidade deve possuir no máximo {1} caracteres!")]
        public string PrestacaoCidade { get; set; }

        [Required(ErrorMessage = "A categoria de serviço deve ser informado!")]
        public int CategoriaServicoId { get; set; }
        public virtual CategoriaServico CategoriaServico { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
