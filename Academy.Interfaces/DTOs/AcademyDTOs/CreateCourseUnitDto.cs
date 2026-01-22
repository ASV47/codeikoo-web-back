using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateCourseUnitDto
	{
        public int CourseId { get; set; }
        public string TilteArabic { get; set; } = default!;
        public string TitleEnglish { get; set; } = default!;
    }
}
