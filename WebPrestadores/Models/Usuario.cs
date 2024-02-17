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

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A cidade do usuário deve ser informada!")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }

        public virtual PrestadorServico PrestadorServico { get; set; }

        public virtual List<PrestadorServicoAvaliacao> ListaPrestadorServicoAvaliacao { get; set; }
    }
}
