using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CreateServiceDTO
	{
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string IconUrl { get; set; } = null!;
	}
}
