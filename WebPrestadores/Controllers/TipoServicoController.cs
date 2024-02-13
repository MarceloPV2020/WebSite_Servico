using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Context;

namespace WebPrestadores.Controllers
{
    public class TipoServicoController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TipoServicoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult List()
        {
            var tiposServicos = _appDbContext.TipoServico;
            ViewBag.QuantidadeListado = tiposServicos.Count();
            return View(tiposServicos);
        }
    }
}
