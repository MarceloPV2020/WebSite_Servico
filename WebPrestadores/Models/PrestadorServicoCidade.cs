using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPrestadores.Models
{
    [Table("PrestadorServicoCidade")]
    public class PrestadorServicoCidade
    {
        [Key]
        public int Id { get; set; }

        public int PrestadorServicoId { get; set; }
        public virtual PrestadorServico PrestadorServico { get; set; }

        [Display(Name = "Nome da Cidade")]
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
