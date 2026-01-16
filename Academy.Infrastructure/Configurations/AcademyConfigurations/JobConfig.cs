using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class JobConfig : BaseConfiguration<Course, int>
	{
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(J => J.EmploymentType)
                   .HasConversion<string>();

			//        builder.Property(j => j.Requirements)
			//.HasConversion(
			//    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
			//    v => string.IsNullOrWhiteSpace(v)
			//        ? new List<string>()
			//        : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
			//)
			//.Metadata.SetValueComparer(
			//    new ValueComparer<List<string>>(
			//        (a, b) => a.SequenceEqual(b),
			//        v => v.Aggregate(0, (acc, x) => HashCode.Combine(acc, (x ?? string.Empty).GetHashCode())),
			//        v => v.ToList()
			//    )
			//);



			//builder.Property(j => j.EmploymentType).HasConversion<string>();

			//// Owned: Title
			//builder.OwnsOne(j => j.Title, title =>
			//{
			//	title.Property(x => x.Ar).HasColumnName("TitleAr").HasMaxLength(200).IsRequired();
			//	title.Property(x => x.En).HasColumnName("TitleEn").HasMaxLength(200).IsRequired();
			//});

			//// Owned: Description
			//builder.OwnsOne(j => j.Description, desc =>
			//{
			//	desc.Property(x => x.Ar).HasColumnName("DescriptionAr").HasColumnType("nvarchar(max)").IsRequired();
			//	desc.Property(x => x.En).HasColumnName("DescriptionEn").HasColumnType("nvarchar(max)").IsRequired();
			//});

			//// Owned: Location
			//builder.OwnsOne(j => j.Location, loc =>
			//{
			//	loc.Property(x => x.Ar).HasColumnName("LocationAr").HasMaxLength(200).IsRequired();
			//	loc.Property(x => x.En).HasColumnName("LocationEn").HasMaxLength(200).IsRequired();
			//});

			//// Owned: Requirements (JSON per language)
			//var converter = new StringListToJsonConverter();
			//var comparer = new StringListValueComparer();

			//builder.OwnsOne(j => j.Requirements, req =>
			//{
			//	req.Property(x => x.Ar)
			//	   .HasColumnName("RequirementsAr")
			//	   .HasColumnType("nvarchar(max)")
			//	   .HasConversion(converter)
			//	   .Metadata.SetValueComparer(comparer);

			//	req.Property(x => x.En)
			//	   .HasColumnName("RequirementsEn")
			//	   .HasColumnType("nvarchar(max)")
			//	   .HasConversion(converter)
			//	   .Metadata.SetValueComparer(comparer);
			//});

			builder.Property(j => j.EmploymentType).HasConversion<string>();

			builder.OwnsOne(j => j.Title, t =>
			{
				t.Property(x => x.Ar).HasColumnName("TitleAr").HasMaxLength(200).IsRequired();
				t.Property(x => x.En).HasColumnName("TitleEn").HasMaxLength(200).IsRequired();
			});

			builder.OwnsOne(j => j.Description, d =>
			{
				d.Property(x => x.Ar).HasColumnName("DescriptionAr").HasColumnType("nvarchar(max)").IsRequired();
				d.Property(x => x.En).HasColumnName("DescriptionEn").HasColumnType("nvarchar(max)").IsRequired();
			});

			builder.OwnsOne(j => j.Location, l =>
			{
				l.Property(x => x.Ar).HasColumnName("LocationAr").HasMaxLength(200).IsRequired();
				l.Property(x => x.En).HasColumnName("LocationEn").HasMaxLength(200).IsRequired();
			});

			var converter = new StringListToJsonConverter();
			var comparer = new StringListValueComparer();

			builder.OwnsOne(j => j.Requirements, r =>
			{
				r.Property(x => x.Ar)
				 .HasColumnName("RequirementsAr")
				 .HasColumnType("nvarchar(max)")
				 .HasConversion(converter)
				 .Metadata.SetValueComparer(comparer);

				r.Property(x => x.En)
				 .HasColumnName("RequirementsEn")
				 .HasColumnType("nvarchar(max)")
				 .HasConversion(converter)
				 .Metadata.SetValueComparer(comparer);
			});

		}
    }
}
