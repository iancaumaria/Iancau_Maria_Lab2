using System.ComponentModel.DataAnnotations;

namespace Iancau_Maria_Lab2.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string PublisherName { get; set; } = string.Empty;
    }
}
