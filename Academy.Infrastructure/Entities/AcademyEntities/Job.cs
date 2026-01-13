using Academy.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class Job : BaseEntity<int>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Location { get; set; } = default!;
        public EmploymentType EmploymentType { get; set; }
        public DateTime PostedAt { get; set; }
        public List<string> Requirements { get; set; } = new();
    }
}
