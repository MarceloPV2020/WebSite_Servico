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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,ImagemUrl,PrestacaoCidade,CategoriaServicoId")] PrestadorServico prestadorServico)
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

        private bool PrestadorExists(int id)
        {
            return _context.PrestadorServico.Any(e => e.Id == id);
        }
    }
}
