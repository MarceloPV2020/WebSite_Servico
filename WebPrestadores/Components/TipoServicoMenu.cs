using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Repositories.Interfaces;

namespace WebPrestadores.Components
{
    public class TipoServicoMenu : ViewComponent
    {
        private readonly ITipoServicoRepository _tipoServicoRepository;

        public TipoServicoMenu(ITipoServicoRepository tipoServicoRepository)
        {
            _tipoServicoRepository = tipoServicoRepository;
        }

        public IViewComponentResult Invoke()
        {
            var tipoServicos = _tipoServicoRepository.TiposServicos.OrderBy(c => c.Nome);
            return View(tipoServicos);
        }
    }
}
