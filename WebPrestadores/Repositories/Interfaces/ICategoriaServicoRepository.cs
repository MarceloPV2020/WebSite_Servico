using WebPrestadores.Models;

namespace WebPrestadores.Repositories.Interfaces
{
    public interface ICategoriaServicoRepository
    {
        IEnumerable<CategoriaServico> CategoriasServico { get; }

    }
}
