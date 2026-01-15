using Academy.Infrastructure.Entities.AcademyEntities;
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
		public async Task<JobDto> AddAsync(CreateJobDto dto)
		{
			var entity = _mapper.Map<Job>(dto);

			// ✅ Audit (Created)
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await _unitOfWork.GetRepository<Job, int>().AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<JobDto>(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Job, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) return false;
			if (entity.IsDeleted) return true;

			// ✅ SoftDelete
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<JobDto>> GetAllAsync()
		{
			var entities = await _unitOfWork.GetRepository<Job, int>()
				.Query()
				.AsNoTracking()
				.Where(x => !x.IsDeleted)           // ✅ SoftDelete filter
				.OrderByDescending(x => x.Id)
				.ToListAsync();

			return _mapper.Map<IEnumerable<JobDto>>(entities);
		}

		public async Task<JobDto?> GetByIdAsync(int id)
		{
			var entity = await _unitOfWork.GetRepository<Job, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted); // ✅ SoftDelete filter

			return entity is null ? null : _mapper.Map<JobDto>(entity);
		}

		// ✅ Restore
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
	}

}
