using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CreateExperienceItemDto
	{
		public string ItemName { get; set; } = default!;
		public int ExperienceCategoryId { get; set; }
	}
}
