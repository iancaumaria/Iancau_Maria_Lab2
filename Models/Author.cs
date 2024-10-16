using System.ComponentModel.DataAnnotations;

namespace Iancau_Maria_Lab2.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}

