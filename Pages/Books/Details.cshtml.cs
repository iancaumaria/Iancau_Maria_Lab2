using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public DetailsModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books
                .Include(b => b.Publisher)  
                .Include(b => b.Author)      
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
