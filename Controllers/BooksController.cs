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
                .Where(b => b.BookCategories.Any(bc => bc.CategoryID == categoryId)) // Filters books by category
                .Include(b => b.Author) // Ensure you have an Author navigation property in your Book model
                .Include(b => b.BookCategories) // Ensure you include the BookCategories relationship to filter
                .ThenInclude(bc => bc.Category) // Include Category details within BookCategories
                .ToList();

            return View(books);
        }
    }
}
