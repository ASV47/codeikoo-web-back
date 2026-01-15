using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Infrastructure.Entities;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class WebSettings : BaseEntity<int>
	{
		public string? HomeTitle { get; set; } = default!;
		public string? ServiceTitle { get; set; } = default!;
		public string? ServiceDescription { get; set; } = default!;
		public string? ClientName { get; set; } = default!;
		public string? ClientJop { get; set; } = default!;
		public string? ClientDescription { get; set; } = default!;
	}
}
