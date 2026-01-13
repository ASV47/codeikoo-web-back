using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class Service : BaseEntity<int>
	{
		public string IconUrl { get; set; } = default!;
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
	}
}
// D:\CodeikooCompany\CodeikooBackend\Company.Solution\Company.Web\wwwroot\Images\Logo\angular-logo-png_seeklogo-272812.png