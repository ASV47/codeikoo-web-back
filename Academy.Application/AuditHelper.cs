using Academy.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application
{
    public static class AuditHelper
    {
		public static void SetCreated<TKey>(BaseEntity<TKey> entity, string userId)
		{
			entity.CreatedBy ??= userId;
			entity.CreatedOn ??= DateTime.UtcNow;
			entity.IsDeleted = false;
		}

		public static void SetModified<TKey>(BaseEntity<TKey> entity, string userId)
		{
			entity.LastModifiedBy = userId;
			entity.LastModifiedOn = DateTime.UtcNow;
		}

		public static void SetSoftDeleted<TKey>(BaseEntity<TKey> entity, string userId)
		{
			entity.IsDeleted = true;
			entity.LastModifiedBy = userId;
			entity.LastModifiedOn = DateTime.UtcNow;
		}
	}
}
