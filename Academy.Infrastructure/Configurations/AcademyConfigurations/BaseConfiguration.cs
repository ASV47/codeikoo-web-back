using Academy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Configurations.AcademyConfigurations
{
	public class BaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<TKey>
	{
		public void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(e => e.CreatedOn)
			   .HasDefaultValueSql("GETDATE()"); // لو ماحطتش قيمة من السيرفيس

			// LastModifiedOn هيتكتب من السيرفيس => سيبه عادي بدون Computed
			builder.Property(e => e.LastModifiedOn)
				   .IsRequired(false);

			builder.Property(e => e.CreatedBy).HasMaxLength(450);
			builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

			builder.Property(e => e.IsDeleted)
				   .HasDefaultValue(false);
		}
	}
}
