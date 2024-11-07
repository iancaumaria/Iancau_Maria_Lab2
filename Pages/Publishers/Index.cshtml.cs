using Iancau_Maria_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models.ViewModels;

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

        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books)
            .ThenInclude(c => c.Author)
            .OrderBy(i => i.PublisherName)
            .ToListAsync();
            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                .Where(i => i.Id == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }
        }
    }
}
