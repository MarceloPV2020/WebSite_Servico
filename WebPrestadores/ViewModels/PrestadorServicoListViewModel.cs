using WebPrestadores.Models;

namespace WebPrestadores.ViewModels
{
    public class PrestadorServicoListViewModel
    {
        public IEnumerable<PrestadorServico> PrestadoresServico { get; set; }
        public string CategoriaServicoAtual { get; set; }
    }
}
