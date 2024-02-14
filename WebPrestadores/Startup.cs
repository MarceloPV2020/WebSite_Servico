using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Repositories;
using WebPrestadores.Repositories.Interfaces;
using WebPrestadores.Services;

namespace WebPrestadores;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        services.AddTransient<ITipoServicoRepository, TipoServicoRepository>();
        services.AddTransient<IPrestadorServicoRepository, PrestadorServicoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
            options.AddPolicy("User",
                politica =>
                {
                    politica.RequireRole("User");
                });
        });
        services.AddSession();
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseSession();
        seedUserRoleInitial.SeedRoles();
        seedUserRoleInitial.SeedUsers();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
               name: "tipoServicoFiltro",
               pattern: "PrestadorServico/{action}/{tipoServico?}",
               defaults: new { Controller = "PrestadorServico", action = "List" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}