using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateContactMessageDto
	{
		public string FullName { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Message { get; set; } = default!;
	}
}
