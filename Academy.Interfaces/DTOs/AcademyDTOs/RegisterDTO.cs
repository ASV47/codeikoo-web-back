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
		public string Password { get; set; } = default!;
		[Required(ErrorMessage = "UserName Is Required")]
		public string UserName { get; set; } = default!;
		[Required(ErrorMessage = "DisplayName Is Required")]
		public string DisplayName { get; set; } = default!;
		[Required(ErrorMessage = "PhoneNumber Is Required")]
		[Phone]
		public string PhoneNumber { get; set; } = default!;
	}
}
