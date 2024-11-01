using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public IndexModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Books = await _context.Books
                    .Include(b => b.Author) 
                    .Include(b => b.Publisher) 
                    .ToListAsync();
            }
        }
    }
}
