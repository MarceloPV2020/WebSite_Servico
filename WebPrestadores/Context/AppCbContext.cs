using Microsoft.EntityFrameworkCore;
using WebPrestadores.Models;

namespace WebPrestadores.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
