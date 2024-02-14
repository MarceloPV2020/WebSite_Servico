using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Context;

namespace WebPrestadores.Controllers
{
    public class CategoriaServicoController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaServicoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult List()
        {
            var categorias = _appDbContext.CategoriaServico;
            ViewBag.QuantidadeListado = categorias.Count();
            return View(categorias);
        }
    }
}
