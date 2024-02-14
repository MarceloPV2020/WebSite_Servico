using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Repositories
{
    public class PrestadorServicoRepository : IPrestadorServicoRepository
    {
        private readonly AppDbContext _context;
        public PrestadorServicoRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<PrestadorServico> Prestadores => _context.PrestadorServico.Include(c => c.CategoriaServico);

        public PrestadorServico GetPrestadorById(int prestadorId)
        {
            return _context.PrestadorServico.FirstOrDefault(l => l.Id == prestadorId);
        }
    }
}
