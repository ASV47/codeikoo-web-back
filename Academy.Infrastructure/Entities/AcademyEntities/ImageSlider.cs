using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class ImageSlider : BaseEntity<int>
    {
        public string? ImageUrl { get; set; }
        public string? ImagePanar { get; set; }
        public string? Email { get; set; }
    }
}
