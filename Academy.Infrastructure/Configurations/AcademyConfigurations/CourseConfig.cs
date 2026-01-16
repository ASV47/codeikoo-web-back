using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Infrastructure.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Configurations.AcademyConfigurations
{
    public class CourseConfig : BaseConfiguration<Course, int>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses", AcademySchema.AcademyDBSchema);

            builder.HasKey(x => x.Id);

			builder.Property(x => x.Duration)
			   .IsRequired();

			builder.Property(x => x.UserId)
				   .IsRequired()
				   .HasMaxLength(450);

			builder.Property(x => x.CourseImage)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.HasOne<ApplicationUser>()
				   .WithMany()
				   .HasForeignKey(x => x.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany<CourseUnit>()
				   .WithOne()
				   .HasForeignKey(x => x.CourseId)
				   .OnDelete(DeleteBehavior.Cascade);

			// ✅ Title (Owned) -> نفس الجدول
			builder.OwnsOne(x => x.Title, t =>
			{
				t.Property(p => p.Ar).HasColumnName("TitleAr").IsRequired().HasMaxLength(200);
				t.Property(p => p.En).HasColumnName("TitleEn").IsRequired().HasMaxLength(200);
			});

			// ✅ Description (Owned) -> نفس الجدول
			builder.OwnsOne(x => x.Description, d =>
			{
				d.Property(p => p.Ar).HasColumnName("DescriptionAr").IsRequired().HasMaxLength(4000);
				d.Property(p => p.En).HasColumnName("DescriptionEn").IsRequired().HasMaxLength(4000);
			});

			// ✅ Features (Owned + JSON لكل لغة) -> نفس الجدول
			var converter = new StringListToJsonConverter();
			var comparer = new StringListValueComparer();

			builder.OwnsOne(x => x.Features, f =>
			{
				f.Property(p => p.Ar)
				 .HasColumnName("FeaturesAr")
				 .HasColumnType("nvarchar(max)")
				 .HasConversion(converter)
				 .Metadata.SetValueComparer(comparer);

				f.Property(p => p.En)
				 .HasColumnName("FeaturesEn")
				 .HasColumnType("nvarchar(max)")
				 .HasConversion(converter)
				 .Metadata.SetValueComparer(comparer);
			});
		}
    }
}
