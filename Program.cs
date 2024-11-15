using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Iancau_Maria_Lab2.Models;
using Iancau_Maria_Lab2.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

internal class Program
{
   

    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
           policy.RequireRole("Admin"));
        });


        // Adaugă servicii în container.
        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Books");
            options.Conventions.AllowAnonymousToPage("/Books/Index");
            options.Conventions.AllowAnonymousToPage("/Books/Details");
            options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
        });
        // Configurare pentru DbContext principal
        builder.Services.AddDbContext<Iancau_Maria_Lab2Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Iancau_Maria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Iancau_Maria_Lab2Context' not found.")));

        // Configurare pentru Identity
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<Iancau_Maria_Lab2Context>();

        // Adaugă suport pentru controlere
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Creează rolul Admin dacă nu există
            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }

            // Caută utilizatorul admin@gmail.com
            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
            if (adminUser != null)
            {
                // Atribuie utilizatorului rolul Admin dacă nu are deja
                var hasRole = await userManager.IsInRoleAsync(adminUser, "Admin");
                if (!hasRole)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        // Configurează pipeline-ul de procesare a cererilor HTTP
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Redirecționează cererile HTTP la HTTPS
        app.UseHttpsRedirection();

        // Servește fișierele implicite și fișierele statice din wwwroot
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();

        // Configurare pentru autentificare și autorizare
        app.UseAuthentication();
        app.UseAuthorization();

        // Configurează rutele pentru paginile Razor și pentru controlerele API
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
        });

        app.Run();
    }
}
