using Academy.Infrastructure.LangHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class Course : BaseEntity<int>
    {
		public LocalizedString Title { get; set; } = new();
		public LocalizedString Description { get; set; } = new();
		public string CourseImage { get; set; } = default!;
        public int Duration { get; set; }
        public string UserId { get; set; } = default!;
		public LocalizedStringList Features { get; set; } = new();
	}
}
