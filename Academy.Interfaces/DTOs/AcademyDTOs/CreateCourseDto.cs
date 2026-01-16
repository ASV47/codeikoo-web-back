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
		public string TitleAr { get; set; } = default!;
		public string TitleEn { get; set; } = default!;
		public string DescriptionAr { get; set; } = default!;
		public string DescriptionEn { get; set; } = default!;
		public int Duration { get; set; }
		public List<string> FeaturesAr { get; set; } = new();
		public List<string> FeaturesEn { get; set; } = new();
		public IFormFile? CourseImage { get; set; }
	}
}
