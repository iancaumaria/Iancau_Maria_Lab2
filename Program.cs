using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Iancau_Maria_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

// Adaugă servicii în container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Iancau_Maria_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Iancau_Maria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Iancau_Maria_Lab2Context' not found.")));

// Adaugă suport pentru controlere
builder.Services.AddControllers();

var app = builder.Build();

// Configurează pipeline-ul de procesare a cererilor HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Redirecționează cererile HTTP la HTTPS
app.UseHttpsRedirection();

// Servește fișierele implicite (ex. index.html) și alte fișiere statice din wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurează rutele pentru paginile Razor și pentru controlerele API

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages(); // Or MapDefaultControllerRoute() if you're using MVC
});

app.Run();

