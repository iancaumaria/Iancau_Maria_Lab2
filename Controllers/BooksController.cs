using Microsoft.EntityFrameworkCore; // Add this line

using Microsoft.AspNetCore.Mvc;
using Iancau_Maria_Lab2.Models;
using Iancau_Maria_Lab2.Data;
using System.Linq;
namespace Iancau_Maria_Lab2.Controllers
{
    public class BooksController : Controller
    {
        private readonly Iancau_Maria_Lab2Context _context; // Update with your DbContext name

    public BooksController(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        // GET: Books by Category
        public IActionResult Index(int categoryId)
        {
            var books = _context.Book
                .Where(b => b.BookCategories.Any(bc => bc.CategoryID == categoryId))
                .Include(b => b.Author) // Include Author details
                .ToList();

            return View(books);
        }
    }
}
