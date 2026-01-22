using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateUnitLessonDto
	{
        public int CourseUnitId { get; set; }
        public string TilteArabic { get; set; } = default!;
        public string TitleEnglish { get; set; } = default!;
    }
}
