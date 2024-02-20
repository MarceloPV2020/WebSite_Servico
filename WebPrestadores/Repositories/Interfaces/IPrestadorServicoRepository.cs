using WebPrestadores.Models;
using WebPrestadores.ViewModels;

namespace WebPrestadores.Repositories.Interfaces
{
    public interface IPrestadorServicoRepository
    {
        IEnumerable<PrestadorServico> Prestadores { get; }
        PrestadorServico GetPrestadorById(int prestadorId);
        PrestadorServicoListViewModel GetPrestadorServicoListViewModel(string aspNetUsersId, string categoria);
        PrestadorServicoListViewModel GetPrestadorServicoListViewModelBySearchNome(string aspNetUsersId, string searchNomeString);
        PrestadorServicoListViewModel GetPrestadorServicoListViewModelBySearchCategoria(string aspNetUsersId, string searchCategoriaString);
    }
}
