using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceLayer.Data.Configurations
{
	public class ExperienceItemConfig : IEntityTypeConfiguration<ExperienceItem>
	{
		public void Configure(EntityTypeBuilder<ExperienceItem> builder)
		{
			builder.HasOne(EI => EI.ExperienceCategory)
				   .WithMany()
				   .HasForeignKey(EI => EI.ExperienceCategoryId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
