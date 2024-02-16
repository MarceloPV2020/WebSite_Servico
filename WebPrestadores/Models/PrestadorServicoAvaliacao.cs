using System.ComponentModel.DataAnnotations;

namespace WebPrestadores.Models
{
    public class PrestadorServicoAvaliacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a observação!")]
        [Display(Name = "Observações")]
        [MinLength(5, ErrorMessage = "Observação deve possuir no mínimo {1} caracteres!")]
        [MaxLength(200, ErrorMessage = "Observação deve possuir no máximo {1} caracteres!")]
        public string Observacao { get; set; }

        [Range(1, 5, ErrorMessage = "A nota deve estar entre 1 e 5")]
        public int Nota { get; set; }

        [Display(Name = "Data da Avaliação")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataAvaliado { get; set; }

        public int PrestadorServicoId { get; set; }
        public virtual PrestadorServico PrestadorServico { get; set; }

        public int UsuarioAvaliadorId { get; set; }
        public virtual Usuario UsuarioAvaliador { get; set; }
    }
}
