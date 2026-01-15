using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CreateAchievementDTO
	{
		public string Title { get; set; } = default!;
		public int Number { get; set; }
	}
}
