using Academy.Infrastructure.Entities.AcademyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Configurations.AcademyConfigurations
{
    public class JobApplicationConfig : BaseConfiguration<Course, int>
	{
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasOne(JA => JA.Job)
                   .WithMany()
                   .HasForeignKey(JA => JA.JobId);

            //builder.HasOne(JA => JA.User)
            //	   .WithMany()
            //	   .HasForeignKey(JA => JA.UserId);
        }
    }
}
