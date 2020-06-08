using System.ComponentModel.DataAnnotations;
using System.IO;

namespace HealthCatalystBackend.Models
{
    public class Account : BaseEntity
    {
        [Key]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        public string Interests { get; set; }

        public string ImageUrl { get; set; }

    }
}