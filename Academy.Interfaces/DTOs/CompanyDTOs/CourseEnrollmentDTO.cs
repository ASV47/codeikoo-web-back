using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CourseEnrollmentDTO
	{
		[Required(ErrorMessage = "Student Name is Required")]
		public string StudentName { get; set; } = default!;

		[Required(ErrorMessage = "Student Email is Required")]
		[EmailAddress]
		public string StudentEmail { get; set; } = default!;

		[Required(ErrorMessage = "PhoneNumber is Required")]
		[Phone]
		public string PhoneNumber { get; set; } = default!;

		[Required(ErrorMessage = "Course ID is Required")]
		public int CourseId { get; set; }
	}
}
