using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class JobService(IUnitOfWork _unitOfWork, IMapper _mapper) : IJobService
	{
		
		public async Task<JobDto> AddAsync(CreateJobDto dto, string? lang = "en")
		{
			var entity = _mapper.Map<Job>(dto);

			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await _unitOfWork.GetRepository<Job, int>().AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return ToDto(entity, lang);
		}

		public async Task<JobDto> UpdateAsync(int id, CreateJobDto dto, string? lang = "en")
		{
			var repo = _unitOfWork.GetRepository<Job, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null || entity.IsDeleted)
				throw new ArgumentException("Job not found.");

			_mapper.Map(dto, entity);

			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();

			return ToDto(entity, lang);
		}

		public async Task<IEnumerable<JobDto>> GetAllAsync(string? lang = "en")
		{
			return await _unitOfWork.GetRepository<Job, int>()
				.Query()
				.AsNoTracking()
				.Where(x => !x.IsDeleted)
				.OrderByDescending(x => x.Id)
				.SelectLocalized(lang)
				.ToListAsync();
		}

		public async Task<JobDto?> GetByIdAsync(int id, string? lang = "en")
		{
			return await _unitOfWork.GetRepository<Job, int>()
				.Query()
				.AsNoTracking()
				.Where(x => x.Id == id && !x.IsDeleted)
				.SelectLocalized(lang)
				.FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Job, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) return false;
			if (entity.IsDeleted) return true;

			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> RestoreAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Job, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) return false;
			if (!entity.IsDeleted) return true;

			entity.IsDeleted = false;
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		private static JobDto ToDto(Job x, string? lang)
		{
			var isAr = LangHelper.IsArabic(lang);

			return new JobDto
			{
				Id = x.Id,
				Title = isAr ? x.Title.Ar : x.Title.En,
				Description = isAr ? x.Description.Ar : x.Description.En,
				Location = isAr ? x.Location.Ar : x.Location.En,
				EmploymentType = x.EmploymentType,
				PostedAt = x.PostedAt,
				Requirements = isAr ? x.Requirements.Ar : x.Requirements.En
			};
		}
	}

}
