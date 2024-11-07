using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;
using System.Threading.Tasks;

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public DetailsModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; }
        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category) // Include categories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            Categories = Book.BookCategories.Select(bc => bc.Category).ToList(); // Get the categories
            return Page();
        }
    }
}
