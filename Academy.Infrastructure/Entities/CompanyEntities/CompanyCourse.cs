using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Infrastructure.Entities;

namespace CoreLayer.Entities
{
	public class CompanyCourse : BaseEntity<int>
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
    }
}
