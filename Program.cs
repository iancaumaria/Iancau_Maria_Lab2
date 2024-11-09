using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Iancau_Maria_Lab2.Data;
using Microsoft.AspNetCore.Identity;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adaugă servicii în container.
        builder.Services.AddRazorPages();

        // Configurare pentru DbContext principal
        builder.Services.AddDbContext<Iancau_Maria_Lab2Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Iancau_Maria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Iancau_Maria_Lab2Context' not found.")));

        // Configurare pentru Identity
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<Iancau_Maria_Lab2Context>();

        // Adaugă suport pentru controlere
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

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
