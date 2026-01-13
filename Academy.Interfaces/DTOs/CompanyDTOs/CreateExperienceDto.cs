using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CreateExperienceDto
	{
		public string Title { get; set; } = default!;
		public List<string> Content { get; set; } = new();
	}
}
