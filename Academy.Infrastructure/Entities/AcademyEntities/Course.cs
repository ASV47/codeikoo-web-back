using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string CourseImage { get; set; } = default!;
        public int Duration { get; set; }
        public string UserId { get; set; } = default!;
        public List<string> Features { get; set; } = new();
    }
}
