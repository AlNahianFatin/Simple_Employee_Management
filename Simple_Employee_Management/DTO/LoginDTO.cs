using System.ComponentModel.DataAnnotations;

namespace Simple_Employee_Management.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(100, ErrorMessage = "Email must be at most 100 characters long.")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        [MaxLength(100, ErrorMessage = "Password must be at most 100 characters long.")]
        public string Password { get; set; } = null!;

        public bool remember { get; set; } = false;
    }
}