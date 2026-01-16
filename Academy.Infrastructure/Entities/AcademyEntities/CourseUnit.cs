using Academy.Infrastructure.LangHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class CourseUnit : BaseEntity<int>
    {
		public LocalizedString Title { get; set; } = new();
		public int CourseId { get; set; }
    }
}
