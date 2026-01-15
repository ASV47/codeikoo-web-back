using Academy.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class Achievement : BaseEntity<int>
	{
		public string Title { get; set; } = default!;
        public int Number { get; set; }
    }
}
