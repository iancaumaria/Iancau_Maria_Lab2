using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iancau_Maria_Lab2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublishingDate { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; } = default;

        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; } = default;
    }
}

