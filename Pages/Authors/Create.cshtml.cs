using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context _context;

        public CreateModel(Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Author.Add(Author);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
