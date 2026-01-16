using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.LangHelper
{
	public class LangHelper
	{
		public static bool IsArabic(string? lang)
		{
			lang = (lang ?? "").Trim().ToLower();
			return lang.StartsWith("ar");
		}
	}
}
