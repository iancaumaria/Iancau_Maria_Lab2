using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iancau_Maria_Lab2.Data; // Ensure this directive exists
using Iancau_Maria_Lab2.Models; // Ensure this directive exists
using System.Collections.Generic;
using System.Linq; // Required for LINQ queries
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Required for Include method

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public IndexModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; }
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            // Initialize sorting and filtering
            TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;

            // Fetch books with related data
            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category) // Fixed variable name for clarity
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s =>
                    s.Author.FirstName.Contains(searchString) ||
                    s.Author.LastName.Contains(searchString) ||
                    s.Title.Contains(searchString)).ToList();
            }

            // Find specific book if ID is provided
            if (id.HasValue)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .SingleOrDefault(i => i.Id == BookID); // Using SingleOrDefault for safety

                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }

            // Sort books based on the selected order
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author.FullName).ToList();
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName).ToList();
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title).ToList();
                    break;
            }
        }
    }
}
