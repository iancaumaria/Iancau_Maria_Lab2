using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Iancau_Maria_Lab2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishingDate { get; set; }

        public Author? Author { get; set; }
        public int? AuthorID { get; set; }

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}

