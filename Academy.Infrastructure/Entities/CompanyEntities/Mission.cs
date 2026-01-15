using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class Mission : BaseEntity<int>
	{
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
    }
}
