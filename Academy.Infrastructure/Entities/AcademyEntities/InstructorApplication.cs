using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class InstructorApplication : BaseEntity<int>
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Specialization { get; set; }
        [Phone]
        public string PhoneNumber { get; set; } = default!;
        public string LinkedInUrl { get; set; }
        public string? CvFilePath { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
