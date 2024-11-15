using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Iancau_Maria_Lab2.Pages.Categories
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context _context;

        public CreateModel(Iancau_Maria_Lab2.Data.Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookID"] = new SelectList(_context.Book, "Id", "Id");
        ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "ID");
            return Page();
        }

        [BindProperty]
        public BookCategory BookCategory { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BookCategory.Add(BookCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
