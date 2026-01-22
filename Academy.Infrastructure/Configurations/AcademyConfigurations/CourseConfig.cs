using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Infrastructure.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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


            var stringListConverter = new ValueConverter<List<string>, string>(
        v => JsonSerializer.Serialize(v ?? new(), (JsonSerializerOptions?)null),
        v => string.IsNullOrWhiteSpace(v)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
    );

            
                builder.Property(x => x.FeaturesAr).HasConversion(stringListConverter).HasColumnType("nvarchar(max)");
                builder.Property(x => x.FeaturesEn).HasConversion(stringListConverter).HasColumnType("nvarchar(max)");
        }
    }
}
