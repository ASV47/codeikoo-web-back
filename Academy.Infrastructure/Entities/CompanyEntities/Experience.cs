using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Infrastructure.Entities;

namespace CoreLayer.Entities
{
	public class Experience : BaseEntity<int>
	{
		public string Title { get; set; } = default!;
		public List<string> Content { get; set; } = new();
	}
}
