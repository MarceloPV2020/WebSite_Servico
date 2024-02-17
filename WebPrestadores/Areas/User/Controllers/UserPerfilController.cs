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
    public class UserPerfilController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserPerfilController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Usuario usuario = _context.Usuario.Include(x => x.Cidade).FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User));
            if (usuario == null)
            {
                usuario =
                    new Usuario()
                    {
                        AspNetUsersId = _userManager.GetUserId(User),
                        Nome = "<Não Informado>",
                        EnderecoDescricao = "<Não Informado>",
                        EnderecoNumero = "SN",
                        EnderecoCep = "00000-000",
                        EnderecoBairro = "<Não Informado>"
                    };
            }

            return View(usuario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Usuario usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                usuario =
                     new Usuario()
                     {
                         AspNetUsersId = _userManager.GetUserId(User)
                     };
            }
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", usuario?.CidadeId ?? 0);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            // Usuário já é um prestador
            if (!usuario.Prestador && (_context.PrestadorServico.FirstOrDefault(x => x.Usuario.AspNetUsersId == _userManager.GetUserId(User)) != null))
            {
                ModelState.AddModelError("Registro", "Usuário é um prestador de serviço. Opção não pode ser desmarcada. Contate o administrador.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        _context.Add(usuario);
                    }
                    else
                    {
                        _context.Update(usuario);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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

            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", usuario?.CidadeId ?? 0);
            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
