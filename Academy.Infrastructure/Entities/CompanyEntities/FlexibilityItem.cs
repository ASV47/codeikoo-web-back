using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class FlexibilityItem : BaseEntity<int>
	{
		public string IconUrl { get; set; } = default!;
		public string Title { get; set; } = default!;
    }
}
