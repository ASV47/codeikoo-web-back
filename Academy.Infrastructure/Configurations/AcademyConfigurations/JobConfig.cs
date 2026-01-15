using Academy.Infrastructure.Entities.AcademyEntities;
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

            builder.Property(j => j.Requirements)
    .HasConversion(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => string.IsNullOrWhiteSpace(v)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
    )
    .Metadata.SetValueComparer(
        new ValueComparer<List<string>>(
            (a, b) => a.SequenceEqual(b),
            v => v.Aggregate(0, (acc, x) => HashCode.Combine(acc, (x ?? string.Empty).GetHashCode())),
            v => v.ToList()
        )
    );


        }
    }
}
