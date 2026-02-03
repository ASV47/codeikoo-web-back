using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.IServices.IAcademyServices;
using Academy.Interfaces.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class JobApplicationService(IUnitOfWork _unitOfWork,
		IMapper _mapper, IFileStorageService fileStorage) : IJobApplicationService
	{
        public async Task<JobApplicationDto> AddAsync(CreateJobApplicationDto dto)
        {
            // ✅ Job لازم يكون موجود ومش SoftDeleted
            var jobRepo = _unitOfWork.GetRepository<Job, int>();
            var job = await jobRepo.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == dto.JobId && !j.IsDeleted);

            if (job is null) throw new ArgumentException("Job not found.");

            string cvUrl = string.Empty;
            if (dto.CvFile is not null)
                cvUrl = await fileStorage.UploadAsync(dto.CvFile, "JobApplicationCVs");

            var entity = _mapper.Map<JobApplication>(dto);
            entity.CvFilePath = cvUrl;

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

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<PagedResult<JobApplicationDto>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = _unitOfWork.GetRepository<JobApplication, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id); // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = _mapper.Map<List<JobApplicationDto>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<JobApplicationDto>
            {
                Items = items,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = pagination.PageNumber > 1,
                HasNextPage = pagination.PageNumber < totalPages
            };
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
