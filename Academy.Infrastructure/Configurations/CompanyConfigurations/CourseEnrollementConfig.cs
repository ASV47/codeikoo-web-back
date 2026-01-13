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
	public class CourseEnrollementConfig : IEntityTypeConfiguration<CourseEnrollment>
	{
		public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
		{
			builder.HasOne(C => C.Course)
				   .WithMany()
				   .HasForeignKey(C => C.CourseId).OnDelete(DeleteBehavior.Cascade);

		}
	}
}
