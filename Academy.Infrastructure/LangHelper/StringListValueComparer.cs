using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.LangHelper
{
	public class StringListValueComparer : ValueComparer<List<string>>
	{
		public StringListValueComparer() : base(
	   (a, b) => a.SequenceEqual(b),
	   v => v.Aggregate(0, (acc, x) => HashCode.Combine(acc, (x ?? "").GetHashCode())),
	   v => v.ToList()
   )
		{ }
	}
}
