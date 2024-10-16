using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Models;
using System.Threading.Tasks;
using Iancau_Maria_Lab2.Data;

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public EditModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } // Cartea pe care o editezi

        public SelectList Authors { get; set; } // Lista derulantă pentru autori
        public SelectList Publishers { get; set; } // Lista derulantă pentru edituri

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            // Populează listele derulante
            Authors = new SelectList(await _context.Authors.ToListAsync(), "Id", "FullName");
            Publishers = new SelectList(await _context.Publishers.ToListAsync(), "Id", "PublisherName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }

}

