using Academy.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class Client : BaseEntity<int>
    {
		public string LogoUrl { get; set; } = default!;
	}
}
