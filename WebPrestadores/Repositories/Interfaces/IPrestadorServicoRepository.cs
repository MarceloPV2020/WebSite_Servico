using WebPrestadores.Models;

namespace WebPrestadores.Repositories.Interfaces
{
    public interface IPrestadorServicoRepository
    {
        IEnumerable<PrestadorServico> Prestadores { get; }
        PrestadorServico GetPrestadorById(int prestadorId);

    }
}
