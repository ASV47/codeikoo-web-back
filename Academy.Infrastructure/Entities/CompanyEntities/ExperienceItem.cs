using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Academy.Infrastructure.Entities;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
	public class ExperienceItem : BaseEntity<int>
	{
		public string ItemName { get; set; } = default!;
		[ForeignKey("ExperienceCategory")]
        public int ExperienceCategoryId { get; set; }
		public ExperienceCategory ExperienceCategory { get; set; } = default!;
    }
}
