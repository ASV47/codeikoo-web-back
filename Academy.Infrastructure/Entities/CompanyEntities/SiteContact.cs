using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class SiteContact : BaseEntity<int>
	{
		public ContactType Type { get; set; } = default!;
		public string Value { get; set; } = default!;
		public string? Label { get; set; }
	}

	public enum ContactType
	{
		[EnumMember(Value = "phone")]
		Phone,
		[EnumMember(Value = "email")]
		Email,
		[EnumMember(Value = "social")]
		Social
	}
}
