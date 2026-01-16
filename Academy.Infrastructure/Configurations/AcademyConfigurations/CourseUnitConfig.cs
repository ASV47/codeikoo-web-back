using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Configurations.AcademyConfigurations
{
    public class CourseUnitConfig : BaseConfiguration<CourseUnit, int>
    {
        public void Configure(EntityTypeBuilder<CourseUnit> builder)
        {
            builder.ToTable("CourseUnits", AcademySchema.AcademyDBSchema);

            builder.HasKey(x => x.Id);

			builder.Property(x => x.CourseId)
			  .IsRequired();

			builder.OwnsOne(x => x.Title, t =>
			{
				t.Property(p => p.Ar).HasColumnName("TitleAr").IsRequired().HasMaxLength(200);
				t.Property(p => p.En).HasColumnName("TitleEn").IsRequired().HasMaxLength(200);
			});

			builder.HasMany<UnitLesson>()
				   .WithOne()
				   .HasForeignKey(x => x.CourseUnitId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
    }
}
