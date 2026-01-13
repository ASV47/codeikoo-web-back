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
    public class UnitLessonConfig : IEntityTypeConfiguration<UnitLesson>
    {
        public void Configure(EntityTypeBuilder<UnitLesson> builder)
        {
            builder.ToTable("UnitLessons", AcademySchema.AcademyDBSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.CourseUnitId)
                   .IsRequired();
        }
    }
}
