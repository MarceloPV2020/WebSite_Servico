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

        [Display(Name = "Descrição do Serviço")]
        [MaxLength(200, ErrorMessage = "Descrição deve possuir no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o email.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A categoria de serviço deve ser informado!")]
        [Display(Name = "Categoria")]
        public int CategoriaServicoId { get; set; }
        public virtual CategoriaServico CategoriaServico { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public List<PrestadorServicoAvaliacao> ListaPrestadorServicoAvaliacao { get; set; } = new List<PrestadorServicoAvaliacao>();

        public List<PrestadorServicoCidade> ListaPrestadorServicoCidade { get; set; } = new List<PrestadorServicoCidade>();

        [ScaffoldColumn(false)]
        [NotMapped]
        public decimal AvaliacaoQuantidade
        {
            get
            {
                return this.ListaPrestadorServicoAvaliacao.Count();
            }
        }

        [ScaffoldColumn(false)]
        [NotMapped]
        public decimal Avaliacao
        {
            get
            {
                if (this.AvaliacaoQuantidade == 0)
                {
                    return 0;
                }

                return Math.Round(this.ListaPrestadorServicoAvaliacao.Sum(x => x.Nota) / this.AvaliacaoQuantidade, 1);
            }
        }
    }
}
