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
        public string TilteArabic { get; set; } = default!;
        public string TitleEnglish { get; set; } = default!;
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public int Duration { get; set; }

        public List<string> FeaturesAr { get; set; } = new();
        public List<string> FeaturesEn { get; set; } = new();

        public IFormFile? CourseImage { get; set; }
    }
}
