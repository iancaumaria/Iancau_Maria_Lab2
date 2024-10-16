using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;

namespace Iancau_Maria_Lab2.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public CreateModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adaugă publisherul în context
            _context.Publishers.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
