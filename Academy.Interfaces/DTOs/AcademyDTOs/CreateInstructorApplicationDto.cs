using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs
{
	public class CreateInstructorApplicationDto
	{
        [Required]
        public string FullName { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Specialization { get; set; } = default!;

        [Required, Phone]
        public string PhoneNumber { get; set; } = default!;

        [Url]
        public string LinkedInUrl { get; set; } = default!;

        public IFormFile? CvFile { get; set; }
    }
}
