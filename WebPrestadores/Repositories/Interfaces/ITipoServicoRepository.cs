using WebPrestadores.Models;

namespace WebPrestadores.Repositories.Interfaces
{
    public interface ITipoServicoRepository
    {
        IEnumerable<TipoServico> TiposServicos { get; }
    }
}
