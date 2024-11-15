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

    public class EditModel : PageModel
    {
        private readonly Iancau_Maria_Lab2Context _context;

        public EditModel(Iancau_Maria_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; }

        public SelectList Authors { get; set; }
        public SelectList Publishers { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _context.Book.Include(b => b.BookCategories).FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            Authors = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");
            Publishers = new SelectList(await _context.Publisher.ToListAsync(), "ID", "PublisherName");

            PopulateAssignedCategoryData();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookToUpdate = await _context.Book.Include(b => b.BookCategories).FirstOrDefaultAsync(b => b.Id == Book.Id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // Update book properties
            bookToUpdate.Title = Book.Title;
            bookToUpdate.AuthorID = Book.AuthorID;
            bookToUpdate.Price = Book.Price;
            bookToUpdate.PublishingDate = Book.PublishingDate;
            bookToUpdate.PublisherID = Book.PublisherID;

            // Handle category selections
            if (selectedCategories != null)
            {
                var selectedCategoriesHS = new HashSet<string>(selectedCategories);
                var bookCategories = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.CategoryID));

                foreach (var category in _context.Category)
                {
                    if (selectedCategoriesHS.Contains(category.ID.ToString()))
                    {
                        if (!bookCategories.Contains(category.ID))
                        {
                            bookToUpdate.BookCategories.Add(new BookCategory { CategoryID = category.ID });
                        }
                    }
                    else
                    {
                        if (bookCategories.Contains(category.ID))
                        {
                            var categoryToRemove = bookToUpdate.BookCategories.Single(c => c.CategoryID == category.ID);
                            _context.Remove(categoryToRemove);
                        }
                    }
                }
            }

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
