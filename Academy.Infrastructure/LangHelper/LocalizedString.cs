using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.LangHelper
{
	[Owned]
	public class LocalizedString
	{
		public string Ar { get; set; } = string.Empty;
		public string En { get; set; } = string.Empty;

		public string Get(string? lang)
			=> LangHelper.IsArabic(lang) ? (Ar ?? string.Empty) : (En ?? string.Empty);
	}
}
