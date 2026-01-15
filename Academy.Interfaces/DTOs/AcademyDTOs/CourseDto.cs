using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
    public class CourseDto
    {
		public int Id { get; set; }
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string CourseImageUrl { get; set; } = default!;
		public int Duration { get; set; }
		public List<string> Features { get; set; } = new();
	}
}
