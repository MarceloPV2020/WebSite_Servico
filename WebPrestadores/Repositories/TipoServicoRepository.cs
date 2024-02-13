using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Repositories
{
    public class TipoServicoRepository : ITipoServicoRepository
    {
        private readonly AppDbContext _context;
        public TipoServicoRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<TipoServico> TiposServicos => _context.TipoServico;
    }
}
