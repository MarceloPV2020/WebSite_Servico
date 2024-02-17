using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPrestadores.Models;

namespace WebPrestadores.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<CategoriaServico> CategoriaServico { get; set; }
        public DbSet<PrestadorServico> PrestadorServico { get; set; }
        public DbSet<PrestadorServicoAvaliacao> PrestadorServicoAvaliacao { get; set; }
        public DbSet<PrestadorServicoCidade> PrestadorServicoCidade { get; set; }
    }
}
