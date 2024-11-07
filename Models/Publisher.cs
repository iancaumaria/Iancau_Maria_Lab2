using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Iancau_Maria_Lab2.Models
{
    public class Publisher
    {
        public int Id { get; set; } // Consistent naming

        [Required]
        public string PublisherName { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>(); // Changed to 'Books'
    }
}
