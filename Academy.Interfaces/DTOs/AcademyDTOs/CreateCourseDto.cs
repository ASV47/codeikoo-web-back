using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateCourseDto
	{
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Duration { get; set; }
		public List<string> Features { get; set; } = new();
		public IFormFile? CourseImage { get; set; }
	}
}
