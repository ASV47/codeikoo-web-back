using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLayer.DTO
{
	public class CompanyContactMessageDTO
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "PhoneNumber is Required")]
		[Phone]
		public string PhoneNumber { get; set; }
		public string Message { get; set; } = default!;
		[JsonPropertyName("terms_accepted")]
		public bool termsAccepted { get; set; }
	}
}
