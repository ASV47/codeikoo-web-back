using Academy.Infrastructure.LangHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class UnitLesson : LocalizableEntity
    {
        public int CourseUnitId { get; set; }
    }
}
