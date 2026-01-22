using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class LocalizableEntity : BaseEntity<int>
    {
        public string TilteArabic { get; set; } = default!;
        public string TitleEnglish { get; set; } = default!;
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }
    }
}
