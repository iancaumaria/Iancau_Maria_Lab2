using System.ComponentModel.DataAnnotations;

namespace Iancau_Maria_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Book>? Books { get; set; }
    }
}

