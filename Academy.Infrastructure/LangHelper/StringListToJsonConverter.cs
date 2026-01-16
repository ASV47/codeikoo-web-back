using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academy.Infrastructure.LangHelper
{
	public class StringListToJsonConverter : ValueConverter<List<string>, string>
	{
		public StringListToJsonConverter() : base(
		v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
		v => string.IsNullOrWhiteSpace(v)
			? new List<string>()
			: JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
	)
		{ }
	}
}
