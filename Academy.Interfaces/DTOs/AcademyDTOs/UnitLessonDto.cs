using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class UnitLessonDto
	{
		public int Id { get; set; }
		public int CourseUnitId { get; set; }
		public string Title { get; set; } = default!;
	}
}
