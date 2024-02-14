using Microsoft.AspNetCore.Mvc;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;
using WebPrestadores.ViewModels;

namespace WebPrestadores.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private readonly IPrestadorServicoRepository _prestadorServicoRepository;
        public PrestadorServicoController(IPrestadorServicoRepository prestadorServicoRepository)
        {
            _prestadorServicoRepository = prestadorServicoRepository;
        }

        public IActionResult List(string tipoServico)
        {
            IEnumerable<PrestadorServico> prestadoresServico;
            string tipoServicoAtual = string.Empty;

            if (string.IsNullOrEmpty(tipoServico))
            {
                prestadoresServico = _prestadorServicoRepository.Prestadores.OrderBy(l => l.Id);
                tipoServicoAtual = "Todos os prestadores";
            }
            else
            {
                prestadoresServico = _prestadorServicoRepository.Prestadores
                         .Where(l => l.TipoServico.Nome.Equals(tipoServico))
                         .OrderBy(c => c.Nome);

                tipoServicoAtual = tipoServico;
            }

            var prestadoresListViewModel =
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadoresServico,
                    TipoServicoAtual = tipoServicoAtual
                };
            return View(prestadoresListViewModel);
        }

        public ViewResult SearchPorNome(string searchNomeString)
        {
            IEnumerable<PrestadorServico> prestadores;
            string mensagem = string.Empty;

            if (string.IsNullOrEmpty(searchNomeString))
            {
                prestadores = _prestadorServicoRepository.Prestadores.OrderBy(p => p.Id);
                mensagem = "Todos os Prestadores";
            }
            else
            {
                prestadores = _prestadorServicoRepository.Prestadores.Where(p => p.Nome.ToLower().Contains(searchNomeString.ToLower()));
                if (prestadores.Any())
                    mensagem = "Prestadores";
                else
                    mensagem = "Nenhum prestador foi encontrado";
            }

            return View
                ("~/Views/PrestadorServico/List.cshtml",
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadores,
                    TipoServicoAtual = mensagem
                });
        }

        public ViewResult SearchPorCategoria(string searchCategoriaString)
        {
            IEnumerable<PrestadorServico> prestadores;
            string mensagem = string.Empty;

            if (string.IsNullOrEmpty(searchCategoriaString))
            {
                prestadores = _prestadorServicoRepository.Prestadores.OrderBy(p => p.Id);
                mensagem = "Todos os Prestadores";
            }
            else
            {
                prestadores = _prestadorServicoRepository.Prestadores.Where(p => p.TipoServico.Nome.ToLower().Contains(searchCategoriaString.ToLower()));
                if (prestadores.Any())
                    mensagem = "Prestadores";
                else
                    mensagem = "Nenhum prestador foi encontrado";
            }

            return View
                ("~/Views/PrestadorServico/List.cshtml",
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadores,
                    TipoServicoAtual = mensagem
                });
        }
    }
}
