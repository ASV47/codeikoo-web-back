using Academy.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class AboutUs : BaseEntity<int>
	{
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
	}
}
