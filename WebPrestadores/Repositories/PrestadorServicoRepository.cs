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

        public IEnumerable<PrestadorServico> Prestadores
        {
            get
            {
                var prestadorTemp = _context.PrestadorServico.Include(c => c.CategoriaServico).Include(x => x.Usuario);
                foreach (var item in prestadorTemp)
                {
                    item.ListaPrestadorServicoAvaliacao = _context.PrestadorServicoAvaliacao
                        .Include(x => x.UsuarioAvaliador)
                        .Where(x => x.PrestadorServicoId == item.Id)
                        .ToList();
                    item.ListaPrestadorServicoCidade = _context.PrestadorServicoCidade
                         .Include(x => x.Cidade)
                         .Where(x => x.PrestadorServicoId == item.Id)
                         .ToList();
                }

                return prestadorTemp;
            }
        }

        public PrestadorServico GetPrestadorById(int prestadorId)
        {
            return _context.PrestadorServico.FirstOrDefault(l => l.Id == prestadorId);
        }
    }
}
