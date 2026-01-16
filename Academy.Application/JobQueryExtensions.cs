using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application
{
    public static class JobQueryExtensions
    {
		public static IQueryable<JobDto> SelectLocalized(this IQueryable<Job> q, string? lang)
		{
			var isAr = LangHelper.IsArabic(lang);

			return q.Select(x => new JobDto
			{
				Id = x.Id,
				Title = isAr ? x.Title.Ar : x.Title.En,
				Description = isAr ? x.Description.Ar : x.Description.En,
				Location = isAr ? x.Location.Ar : x.Location.En,
				EmploymentType = x.EmploymentType,
				PostedAt = x.PostedAt,
				Requirements = isAr ? x.Requirements.Ar : x.Requirements.En
			});
		}
	}
}
