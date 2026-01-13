using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
    public class WebSettingsDto
    {
		public int Id { get; set; }
		public string? HomeTitle { get; set; }
		public string? ServiceTitle { get; set; }
		public string? ServiceDescription { get; set; }
		public string? ClientName { get; set; }
		public string? ClientJop { get; set; }
		public string? ClientDescription { get; set; }
	}
}
