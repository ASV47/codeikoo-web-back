using Academy.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class CompanyJopApplication : BaseEntity<int>
	{
		[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "PhoneNumber is Required")]
		[Phone]
		public string PhoneNumber { get; set; }
		public string Message { get; set; } = default!;
        public bool termsAccepted { get; set; }
    }
}
