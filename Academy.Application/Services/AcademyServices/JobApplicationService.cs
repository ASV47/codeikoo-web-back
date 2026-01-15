using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class JobApplicationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IJobApplicationService
	{
		public async Task<JobApplicationDto> AddAsync(CreateJobApplicationDto dto)
		{
			// ✅ Job لازم يكون موجود ومش SoftDeleted (لو Job عنده IsDeleted)
			var jobRepo = _unitOfWork.GetRepository<Job, int>();
			var job = await jobRepo.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(j => j.Id == dto.JobId && !j.IsDeleted);

			if (job is null) throw new ArgumentException("Job not found.");

			string cvPath = string.Empty;
			if (dto.CvFile is not null)
				cvPath = DocumentSettings.UploadFile(dto.CvFile, "JobApplicationCVs");

			var entity = _mapper.Map<JobApplication>(dto);
			entity.CvFilePath = cvPath;

			// ✅ Audit (Created)
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await _unitOfWork.GetRepository<JobApplication, int>().AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<JobApplicationDto>(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<JobApplication, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) return false;
			if (entity.IsDeleted) return true;

			// ✅ SoftDelete فقط (بدون حذف الملف)
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<JobApplicationDto>> GetAllAsync()
		{
			var entities = await _unitOfWork.GetRepository<JobApplication, int>()
				.Query()
				.AsNoTracking()
				.Where(x => !x.IsDeleted)         // ✅ SoftDelete filter
				.OrderByDescending(x => x.Id)
				.ToListAsync();

			return _mapper.Map<IEnumerable<JobApplicationDto>>(entities);
		}

		public async Task<JobApplicationDto?> GetByIdAsync(int id)
		{
			var entity = await _unitOfWork.GetRepository<JobApplication, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);  // ✅ SoftDelete filter

			return entity is null ? null : _mapper.Map<JobApplicationDto>(entity);
		}

		// ✅ Restore (زي ما اتفقنا)
		public async Task<bool> RestoreAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<JobApplication, int>();
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
