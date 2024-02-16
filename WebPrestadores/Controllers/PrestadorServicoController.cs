using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;
using WebPrestadores.ViewModels;

namespace WebPrestadores.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private readonly IPrestadorServicoRepository _prestadorServicoRepository;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PrestadorServicoController(IPrestadorServicoRepository prestadorServicoRepository, AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _prestadorServicoRepository = prestadorServicoRepository;
            _context = context;
            _userManager = userManager;
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

            ViewData["Prestador"] = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User))?.Prestador;
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

        public IActionResult ListaAvaliacao(int id)
        {
            var prestadorTemp = _context.PrestadorServico
                .Include(x => x.CategoriaServico)
                .FirstOrDefault(x => x.Id == id);
            prestadorTemp.ListaPrestadorServicoAvaliacao = _context.PrestadorServicoAvaliacao
                .Include(x => x.UsuarioAvaliador)
                .Where(x => x.PrestadorServicoId == id)
                .ToList();
            return View(prestadorTemp);
        }

        public IActionResult Avaliar(int id)
        {
            return View(
                new PrestadorServicoAvaliacaoViewModel()
                {
                    IdPrestadorServico = id
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Avaliar(int idPrestadorServico, [Bind("IdPrestadorServico,Observacao,Nota,DataAvaliado")] PrestadorServicoAvaliacaoViewModel prestadorServicoAvaliacao)
        {
            if (idPrestadorServico != prestadorServicoAvaliacao.IdPrestadorServico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prestador = await _context.PrestadorServico.FindAsync(idPrestadorServico);
                    prestador.ListaPrestadorServicoAvaliacao ??= new List<PrestadorServicoAvaliacao>();
                    PrestadorServicoAvaliacao avaliacao =
                        new PrestadorServicoAvaliacao()
                        {
                            PrestadorServico = prestador,
                            Observacao = prestadorServicoAvaliacao.Observacao,
                            Nota = prestadorServicoAvaliacao.Nota,
                            DataAvaliado = prestadorServicoAvaliacao.DataAvaliado,
                            UsuarioAvaliador = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User))
                        };
                    prestador.ListaPrestadorServicoAvaliacao.Add(avaliacao);
                    _context.Update(prestador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestadorServicoAvaliacao.IdPrestadorServico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(prestadorServicoAvaliacao);
        }

        private bool PrestadorExists(int id)
        {
            return _context.PrestadorServico.Any(e => e.Id == id);
        }
    }
}
