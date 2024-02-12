using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _contexto;

        public UsuarioRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Usuario> Usuarios => _contexto.Usuario.Include(x => x.TipoServico);

        public Usuario GetUsuarioById(int usuarioId)
        {
            return _contexto.Usuario.FirstOrDefault(x => x.Id == usuarioId);
        }
    }
}
