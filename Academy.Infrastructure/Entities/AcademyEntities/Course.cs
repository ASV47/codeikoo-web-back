using Academy.Infrastructure.LangHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class Course : LocalizableEntity
    {
        public string CourseImage { get; set; } = default!;
        public int Duration { get; set; }
        public string UserId { get; set; } = default!;

        public List<string> FeaturesAr { get; set; } = new();
        public List<string> FeaturesEn { get; set; } = new();
    }
}
