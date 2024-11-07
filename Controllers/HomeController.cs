
using Microsoft.AspNetCore.Mvc;
namespace Iancau_Maria_Lab2.Controllers
{
    public class HomeController : Controller
    {
        // Metoda Index care servește ca acțiune pentru ruta implicită
        public IActionResult Index()
        {
            return View();
        }
    }
}
