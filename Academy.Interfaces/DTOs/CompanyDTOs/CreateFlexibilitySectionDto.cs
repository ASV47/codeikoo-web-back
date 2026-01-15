using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CreateFlexibilitySectionDto
	{
		public string Title { get; set; } = default!;
		public string SubTitle { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string SubDescription { get; set; } = default!;
	}
}
