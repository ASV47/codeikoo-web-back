using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CourseUnitDto
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public string Title { get; set; } = default!;
	}
}
