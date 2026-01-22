using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class JobApplicationDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string PortfolioUrl { get; set; } = default!;
		public string CvFilePath { get; set; } = default!;
		public string CoverLetter { get; set; } = default!;
		public DateTime AppliedAt { get; set; }
		public int JobId { get; set; }
	}
}
