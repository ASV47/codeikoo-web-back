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
        public string TilteArabic { get; set; } = default!;
        public string TitleEnglish { get; set; } = default!;
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public string Location { get; set; } = default!;
        public EmploymentType EmploymentType { get; set; }
        public DateTime? PostedAt { get; set; }

        public List<string> RequirementsAr { get; set; } = new();
        public List<string> RequirementsEn { get; set; } = new();
    }
}
