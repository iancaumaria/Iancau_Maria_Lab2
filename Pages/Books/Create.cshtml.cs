using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iancau_Maria_Lab2.Data;
using Iancau_Maria_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Iancau_Maria_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public CreateModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; }

        public SelectList Authors { get; set; }
        public SelectList Publishers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Authors = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");
            Publishers = new SelectList(await _context.Publisher.ToListAsync(), "ID", "PublisherName");

            Book = new Book();
            Book.BookCategories = new List<BookCategory>();

            PopulateAssignedCategoryData();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newBook = new Book();

            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var category in selectedCategories)
                {
                    newBook.BookCategories.Add(new BookCategory
                    {
                        CategoryID = int.Parse(category)
                    });
                }
            }

            Book.BookCategories = newBook.BookCategories;

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private void PopulateAssignedCategoryData()
        {
            var allCategories = _context.Category.ToList();
            AssignedCategoryDataList = allCategories.Select(c => new AssignedCategoryData
            {
                CategoryID = c.ID,
                Name = c.Name
            }).ToList();
        }
    }
}


