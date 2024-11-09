using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context _context;

        public DetailsModel(Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Include Member și Book pentru a le adăuga la detaliile împrumutului
            var borrowing = await _context.Borrowing
                .Include(b => b.Member)  // Include membrul
                .Include(b => b.Book)    // Include cartea
                .ThenInclude(b => b.Publisher) // Dacă vrei să incluzi și editorul cărții
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }

            Borrowing = borrowing;
            return Page();
        }

    }
}
