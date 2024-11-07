using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Ensure this is included for Include() and IQueryable
using Iancau_Maria_Lab2.Data; // Adjust this namespace according to your project structure
using Iancau_Maria_Lab2.Models; // Adjust this namespace according to your project structure

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class BooksByCategoryModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public BooksByCategoryModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; } = new List<Book>(); // List of books for the selected category
        public string CategoryName { get; set; } // Property to hold the category name

        public async Task OnGetAsync(int id) // id would be the category ID
        {
            Books = await _context.Book
                .Include(b => b.Author) // Include the Author details
                .Where(b => b.BookCategories.Any(bc => bc.CategoryID == id)) // Filter by the selected category
                .ToListAsync();

            // Optionally fetch the category name (assuming you have a Categories DbSet in your context)
            var category = await _context.Category.FindAsync(id);
            CategoryName = category?.Name; // Assuming there's a Name property in your Category model
        }
    }

}
