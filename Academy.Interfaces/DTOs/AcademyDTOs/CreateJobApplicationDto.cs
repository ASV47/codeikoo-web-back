using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateJobApplicationDto
	{
		public string FullName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string PortfolioUrl { get; set; } = default!;
		public IFormFile? CvFile { get; set; } // لرفع الملف
		public string CoverLetter { get; set; } = default!;
		public int JobId { get; set; }
	}
}
