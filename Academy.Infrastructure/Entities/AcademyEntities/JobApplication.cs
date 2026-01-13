using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Entities.AcademyEntities
{
    public class JobApplication : BaseEntity<int>
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PortfolioUrl { get; set; } = default!;
        public string CvFilePath { get; set; } = default!;
        public string CoverLetter { get; set; } = default!;
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; } = default!;
    }
}
