using Academy.Infrastructure.Enums;
using Academy.Infrastructure.LangHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class Job : LocalizableEntity
    {
        public string Location { get; set; } = default!;

        public EmploymentType EmploymentType { get; set; }
        public DateTime PostedAt { get; set; }

        public List<string> RequirementsAr { get; set; } = new();
        public List<string> RequirementsEn { get; set; } = new();
    }
}
