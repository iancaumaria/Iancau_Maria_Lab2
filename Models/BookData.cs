namespace Iancau_Maria_Lab2.Models
{
    public class BookData
    {
        public IList<Book> Books { get; set; } = new List<Book>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>(); // Ensure Categories is defined
    }
}
