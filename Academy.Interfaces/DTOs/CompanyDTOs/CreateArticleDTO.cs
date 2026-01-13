using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
	public class CreateArticleDTO
	{
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public IFormFile Image { get; set; } = default!;
	}
}
