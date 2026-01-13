using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class ForgotPasswordResponseDTO
	{
		public string Message { get; set; } = "If the email exists, a reset token was generated.";
		public string? Token { get; set; }
	}
}
