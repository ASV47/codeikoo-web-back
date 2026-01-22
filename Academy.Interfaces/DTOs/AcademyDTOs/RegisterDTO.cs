using Academy.Infrastructure.Entities.AcademyEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
    public class RegisterDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; } = default!;

        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "DisplayName Is Required")]
        public string DisplayName { get; set; } = default!;

        [Required(ErrorMessage = "PhoneNumber Is Required")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone number must be digits only (10-15 digits).")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Governorate is required")]
        public string Governorate { get; set; } = default!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>?)
                validationContext.GetService(typeof(UserManager<ApplicationUser>));

            if (userManager is null)
                yield break;

            // Username uniqueness
            var existingByName = userManager.FindByNameAsync(UserName).GetAwaiter().GetResult();
            if (existingByName is not null)
                yield return new ValidationResult("UserName already exists", new[] { nameof(UserName) });

            // Email uniqueness
            var existingByEmail = userManager.FindByEmailAsync(Email).GetAwaiter().GetResult();
            if (existingByEmail is not null)
                yield return new ValidationResult("Email already exists", new[] { nameof(Email) });

            // Phone uniqueness
            if (userManager.Users.Any(u => u.PhoneNumber == PhoneNumber))
                yield return new ValidationResult("PhoneNumber already exists", new[] { nameof(PhoneNumber) });

        }
    }
}
