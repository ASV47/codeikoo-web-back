using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class RegisterDTO
	{
		[EmailAddress(ErrorMessage = "Email Is Required")]
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
    }
}
