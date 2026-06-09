using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Simple_Employee_Management.DTO
{
    public class AdminDTO
    {
        [ValidateNever]
        [Display(Name="Id")]
        public int AdminId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Username must be at most 100 characters long.")]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        [MaxLength(100, ErrorMessage = "Password must be at most 100 characters long.")]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(250, ErrorMessage = "Email must be at most 250 characters long.")]
        public string Email { get; set; } = null!;
    }
}
