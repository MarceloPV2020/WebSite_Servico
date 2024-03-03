using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserPerfilController(AppDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _context.Usuario.Include(x => x.Cidade).FirstOrDefaultAsync(x => x.AspNetUsersId == _userManager.GetUserId(User));
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
            Usuario usuario = await _context.Usuario.Include(x => x.Cidade).FirstOrDefaultAsync(x => x.Id == id);
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

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(IFormFile fromFiles)
        {
            string path = string.Empty;
            try
            {
                string uploadpath = _webHostEnvironment.WebRootPath;
                string dest_path = Path.Combine(uploadpath, "uploaded_doc");

                if (!Directory.Exists(dest_path))
                {
                    Directory.CreateDirectory(dest_path);
                }
                string sourcefile = Path.GetFileName(fromFiles.FileName);
                path = Path.Combine(dest_path, sourcefile);

                using (FileStream filestream = new FileStream(path, FileMode.Create))
                {
                    fromFiles.CopyTo(filestream);
                }
            }
            catch
            {
                return StatusCode(500, "Arquivo inválido.");
            }

            using var package = new ExcelPackage(new FileInfo(path));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.First();
            var noOfCol = workSheet.Dimension.End.Column;
            var noOfRow = workSheet.Dimension.End.Row;
            for (int rowIterator = 1; rowIterator <= noOfRow; rowIterator++)
            {
                if (workSheet.Cells[rowIterator, 1].Value.ToString() == "Nome Usuario")
                {
                    continue;
                }

                if (_userManager.FindByEmailAsync(workSheet.Cells[rowIterator, 9].Value.ToString()).Result != null)
                {
                    return StatusCode(500, "Usuário já cadastrado");
                }

                IdentityUser user = new IdentityUser();
                user.Email = workSheet.Cells[rowIterator, 9].Value.ToString();
                user.UserName = user.Email;
                user.NormalizedUserName = user.Email.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, workSheet.Cells[rowIterator, 2].Value.ToString()).Result;
                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors.FirstOrDefault().Description);
                }

                _userManager.AddToRoleAsync(user, "User").Wait();

                var cidade = _context.Cidade.FirstOrDefault(x => x.CodigoIbge == Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value))?.Id;
                if (cidade == null)
                {
                    return StatusCode(500, "Cidade não cadastrada");
                }

                Usuario usuario =
                    new Usuario()
                    {
                        AspNetUsersId = user.Id,
                        Nome = workSheet.Cells[rowIterator, 1].Value.ToString(),
                        EnderecoDescricao = workSheet.Cells[rowIterator, 3].Value.ToString(),
                        EnderecoNumero = workSheet.Cells[rowIterator, 4].Value.ToString(),
                        EnderecoBairro = workSheet.Cells[rowIterator, 5].Value.ToString(),
                        EnderecoCep = workSheet.Cells[rowIterator, 6].Value.ToString(),
                        Telefone = workSheet.Cells[rowIterator, 8].Value.ToString(),
                        CidadeId = cidade ?? 0,
                        Email = workSheet.Cells[rowIterator, 9].Value.ToString(),
                        Prestador = true
                    };
                _context.Add(usuario);
                _context.SaveChanges();

                PrestadorServico prestadorServico =
                  new PrestadorServico()
                  {
                      Nome = workSheet.Cells[rowIterator, 10].Value.ToString(),
                      Descricao = workSheet.Cells[rowIterator, 12].Value.ToString(),
                      CategoriaServicoId = _context.CategoriaServico.FirstOrDefault(x => x.Id == Convert.ToInt32(workSheet.Cells[rowIterator, 11].Value)).Id,
                      ImagemUrl = workSheet.Cells[rowIterator, 16].Value?.ToString(),
                      Email = workSheet.Cells[rowIterator, 14].Value.ToString(),
                      Telefone = workSheet.Cells[rowIterator, 13].Value.ToString(),
                      UsuarioId = usuario.Id
                  };
                _context.Add(prestadorServico);
                _context.SaveChanges();

                PrestadorServicoCidade prestadorCidade =
                    new PrestadorServicoCidade()
                    {
                        PrestadorServicoId = prestadorServico.Id,
                        CidadeId = _context.Cidade.FirstOrDefault(x => x.CodigoIbge == Convert.ToInt32(workSheet.Cells[rowIterator, 15].Value)).Id
                    };
                prestadorServico.ListaPrestadorServicoCidade.Add(prestadorCidade);
                _context.Update(prestadorServico);
                _context.SaveChanges();
            }

            return Ok("Registros importados com sucesso");
        }
    }
}
