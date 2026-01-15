using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
	public class CreateClientDto
	{
		public IFormFile Image { get; set; } = null!;
	}
}
