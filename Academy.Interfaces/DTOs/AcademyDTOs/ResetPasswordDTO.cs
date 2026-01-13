using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class ResetPasswordDTO
	{
		[EmailAddress]
		public string Email { get; set; } = default!;
		public string Token { get; set; } = default!;
		public string NewPassword { get; set; } = default!;
	}
}
