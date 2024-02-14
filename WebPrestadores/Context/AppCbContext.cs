﻿using Microsoft.AspNetCore.Identity;
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

        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<PrestadorServico> PrestadorServico { get; set; }
    }
}
