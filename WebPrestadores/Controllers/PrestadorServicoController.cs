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

        public IActionResult List(string categoriaServico)
        {
            IEnumerable<PrestadorServico> prestadoresServico;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoriaServico))
            {
                prestadoresServico = _prestadorServicoRepository.Prestadores.OrderBy(l => l.Id);
                categoriaAtual = "Todos os prestadores";
            }
            else
            {
                prestadoresServico = _prestadorServicoRepository.Prestadores
                         .Where(l => l.CategoriaServico.Nome.Equals(categoriaServico))
                         .OrderBy(c => c.Nome);

                categoriaAtual = categoriaServico;
            }

            var prestadoresListViewModel =
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadoresServico,
                    CategoriaServicoAtual = categoriaAtual
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
                    CategoriaServicoAtual = mensagem
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
                prestadores = _prestadorServicoRepository.Prestadores.Where(p => p.CategoriaServico.Nome.ToLower().Contains(searchCategoriaString.ToLower()));
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
                    CategoriaServicoAtual = mensagem
                });
        }
    }
}
