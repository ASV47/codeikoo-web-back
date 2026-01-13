using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Academy.Infrastructure.Entities;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class CourseEnrollment : BaseEntity<int>
	{
        [Required(ErrorMessage ="Student Name is Required")]
		public string StudentName { get; set; }
		[Required(ErrorMessage = "Student Email is Required")]
		[EmailAddress]
		public string StudentEmail { get; set; }
		[Required(ErrorMessage = "PhoneNumber is Required")]
		[Phone]
		public string PhoneNumber { get; set; }
		public DateTime EnrollmentDate { get; set; } = DateTime.Now;
		[ForeignKey("Course")]
        public int CourseId { get; set; }
		public CompanyCourse Course { get; set; } = default!;
    }
}
