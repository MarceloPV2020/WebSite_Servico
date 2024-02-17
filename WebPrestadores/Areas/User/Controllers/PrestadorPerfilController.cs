using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Models;

namespace WebPrestadores.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class PrestadorPerfilController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PrestadorPerfilController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var prestador = _context.PrestadorServico.FirstOrDefault(x => x.Usuario.AspNetUsersId == _userManager.GetUserId(User));
            return View(prestador);
        }

        public IActionResult Edit(int? id)
        {
            var prestador = _context.PrestadorServico.FirstOrDefault(x => x.Usuario.AspNetUsersId == _userManager.GetUserId(User)) ?? new PrestadorServico();
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaServico, "Id", "Nome", prestador?.CategoriaServicoId ?? 0);
            return View(prestador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrestadorServico prestadorServico)
        {
            if (id != prestadorServico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        prestadorServico.Usuario = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User));
                        _context.Add(prestadorServico);
                    }
                    else
                    {
                        var prestadorTemp = await _context.PrestadorServico.FindAsync(id);
                        prestadorTemp.Nome = prestadorServico.Nome;
                        prestadorTemp.Descricao = prestadorServico.Descricao;
                        prestadorTemp.ImagemUrl = prestadorServico.ImagemUrl;
                        prestadorTemp.CategoriaServicoId = prestadorServico.CategoriaServicoId;
                        _context.Update(prestadorTemp);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestadorServico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaServico, "Id", "Nome", prestadorServico.CategoriaServicoId);
            return View(prestadorServico);
        }

        public async Task<IActionResult> ListaAvaliacao(int id)
        {
            var prestadorTemp = await _context.PrestadorServico
                .Include(x => x.CategoriaServico)
                .FirstOrDefaultAsync(x => x.Id == id);
            prestadorTemp.ListaPrestadorServicoAvaliacao = await _context.PrestadorServicoAvaliacao
                .Include(x => x.UsuarioAvaliador)
                .Where(x => x.PrestadorServicoId == id)
                .ToListAsync();
            return View(prestadorTemp);
        }

        private bool PrestadorExists(int id)
        {
            return _context.PrestadorServico.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IndexCidade(int idPrestacaoServico)
        {
            var prestador = await _context.PrestadorServico.FindAsync(idPrestacaoServico);
            prestador.ListaPrestadorServicoCidade = await _context.PrestadorServicoCidade.Include(x => x.Cidade).Where(x => x.PrestadorServicoId == idPrestacaoServico).ToListAsync();
            return View(prestador);
        }

        public IActionResult AdicionarCidade(int idPrestadorServico)
        {
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", 0);
            return View(
                new PrestadorServicoCidade()
                {
                    PrestadorServicoId = idPrestadorServico
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarCidade(int PrestadorServicoId, PrestadorServicoCidade prestadorServicoCidade)
        {
            if (PrestadorServicoId != prestadorServicoCidade.PrestadorServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prestador = await _context.PrestadorServico.FindAsync(PrestadorServicoId);
                    prestador.ListaPrestadorServicoCidade.Add(
                        new PrestadorServicoCidade()
                        {
                            PrestadorServico = prestador,
                            CidadeId = prestadorServicoCidade.CidadeId
                        });
                    _context.Update(prestador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestadorServicoCidade.PrestadorServicoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", 0);
            return View(prestadorServicoCidade);
        }

        public async Task<IActionResult> DeleteCidade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.PrestadorServicoCidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [HttpPost, ActionName("DeleteCidade")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCidadeConfirmed(int id)
        {
            var cidade = await _context.PrestadorServicoCidade.FindAsync(id);
            _context.PrestadorServicoCidade.Remove(cidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexCidade), new { idPrestacaoServico = cidade.PrestadorServicoId });
        }
    }
}
