using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Components
{
    public class CategoriaServicoMenu : ViewComponent
    {
        private readonly ICategoriaServicoRepository _categoriaServicoRepository;

        public CategoriaServicoMenu(ICategoriaServicoRepository categoriaServicoRepository)
        {
            _categoriaServicoRepository = categoriaServicoRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categoria = _categoriaServicoRepository.CategoriasServico.OrderBy(c => c.Nome);
            return View(categoria);
        }
    }
}
