using Academy.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateJobDto
	{
		//public string Title { get; set; } = default!;
		//public string Description { get; set; } = default!;
		//public string Location { get; set; } = default!;
		//public EmploymentType EmploymentType { get; set; }
		//public List<string> Requirements { get; set; } = new();

		public string TitleAr { get; set; } = default!;
		public string TitleEn { get; set; } = default!;

		public string DescriptionAr { get; set; } = default!;
		public string DescriptionEn { get; set; } = default!;

		public string LocationAr { get; set; } = default!;
		public string LocationEn { get; set; } = default!;

		public EmploymentType EmploymentType { get; set; }
		public DateTime? PostedAt { get; set; }
		public List<string> RequirementsAr { get; set; } = new();
		public List<string> RequirementsEn { get; set; } = new();
	}
}
