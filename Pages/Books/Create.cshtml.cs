using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;
using System.Threading.Tasks;

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public CreateModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Lista pentru Publisher
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "ID", "PublisherName");

            // Lista pentru Author
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FirstName"); // Asigură-te că folosești "FirstName" și "LastName" corect

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName", Book.AuthorID);
                ViewData["PublisherID"] = new SelectList(_context.Publishers, "ID", "PublisherName", Book.PublisherID);
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

