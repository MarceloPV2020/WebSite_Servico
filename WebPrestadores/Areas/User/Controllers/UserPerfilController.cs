using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            Usuario usuario = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User));
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
                        EnderecoBairro = "<Não Informado>",
                        EnderecoCidade = "<Não Informado>",
                        EnderecoUf = "--",
                    };
                _context.Add(usuario);
                await _context.SaveChangesAsync();
            }

            return View(usuario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,AspNetUsersId,EnderecoDescricao,Prestador,Contabilidade,EnderecoDescricao,EnderecoNumero,EnderecoBairro,EnderecoCep,EnderecoCidade,EnderecoUf")] Usuario usuario)
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
                    _context.Update(usuario);
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
            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
