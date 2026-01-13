using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class ExperienceCategory : BaseEntity<int>
	{
		public string CategoryName { get; set; } = default!;
    }
}
