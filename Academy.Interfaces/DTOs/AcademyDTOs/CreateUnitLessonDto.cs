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

		public string TitleAr { get; set; } = default!;
		public string TitleEn { get; set; } = default!;
	}
}
