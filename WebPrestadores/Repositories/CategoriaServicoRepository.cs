using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Repositories
{
    public class CategoriaServicoRepository : ICategoriaServicoRepository
    {
        private readonly AppDbContext _context;
        public CategoriaServicoRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<CategoriaServico> CategoriasServico => _context.CategoriaServico;
    }
}
