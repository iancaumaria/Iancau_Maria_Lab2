using Iancau_Maria_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iancau_Maria_Lab2.Data;

namespace Iancau_Maria_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public IndexModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publishers { get; set; } = new List<Publisher>();

        public async Task OnGetAsync()
        {
            
            Publishers = await _context.Publishers.ToListAsync();
        }
    }
}
