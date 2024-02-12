using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult List()
        {
            var usuarios = _usuarioRepository.Usuarios;
            ViewBag.Total = usuarios.Count();

            return View(usuarios);
        }
    }
}
