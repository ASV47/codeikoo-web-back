using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class Technology : BaseEntity<int>
	{
		public string TechnologyUrl { get; set; } = default!;
	}
}
