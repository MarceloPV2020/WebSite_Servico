using WebPrestadores.Models;

namespace WebPrestadores.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Usuarios { get; }
        Usuario GetUsuarioById(int usuarioId);
    }
}
