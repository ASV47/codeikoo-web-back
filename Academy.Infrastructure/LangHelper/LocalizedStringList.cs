using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.LangHelper
{
	[Owned]
	public class LocalizedStringList
	{
		public List<string> Ar { get; set; } = new();
		public List<string> En { get; set; } = new();

		public List<string> Get(string? lang)
			=> LangHelper.IsArabic(lang) ? (Ar ?? new()) : (En ?? new());
	}
}
