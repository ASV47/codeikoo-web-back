using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class CourseUnit : BaseEntity<int>
    {
        public string Title { get; set; } = default!;
        public int CourseId { get; set; }
    }
}
