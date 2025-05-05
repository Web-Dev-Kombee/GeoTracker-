using System.ComponentModel.DataAnnotations;

namespace MapTask.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Enter First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        [Range(18, 120, ErrorMessage = "Age must be 18 between 120")]
        public int Age { get; set; }

        

    }
}
