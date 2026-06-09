using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Simple_Employee_Management.DTO
{
    public class EmployeeDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "First Name must be at most 100 characters long.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(100, ErrorMessage = "Last Name must be at most 100 characters long.")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(100, ErrorMessage = "Position must be at most 100 characters long.")]
        public string Position { get; set; } = null!;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; }
    }
}
