using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class InstructorApplicationDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Specialization { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string LinkedInUrl { get; set; } = default!;
		public string CvFilePath { get; set; } = default!;
		public DateTime AppliedAt { get; set; }
	}
}
