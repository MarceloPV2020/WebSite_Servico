using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserPerfilController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(_context.Usuario.FirstOrDefault(x => x.AspNetUsersId == _userManager.GetUserId(User)));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaServico categoriaServico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaServico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaServico);
        }
    }
}
