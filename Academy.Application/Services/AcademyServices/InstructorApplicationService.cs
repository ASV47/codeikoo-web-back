using Academy.Application.Repositories;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class InstructorApplicationService(IUnitOfWork _unitOfWork,
		IMapper _mapper, IFileStorageService fileStorage) : IInstructorApplicationService
	{
        public async Task<InstructorApplicationDto> AddAsync(CreateInstructorApplicationDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            string cvUrl = string.Empty;

            if (dto.CvFile is not null)
                cvUrl = await fileStorage.UploadAsync(dto.CvFile, "InstructorCVs");

            var entity = _mapper.Map<InstructorApplication>(dto);
            entity.CvFilePath = cvUrl;

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await _unitOfWork.GetRepository<InstructorApplication, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InstructorApplicationDto>(entity);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<InstructorApplication, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null) return false;
            if (entity.IsDeleted) return true;

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }



        public async Task<PagedResult<InstructorApplicationDto>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = _unitOfWork.GetRepository<InstructorApplication, int>()
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

            var items = _mapper.Map<List<InstructorApplicationDto>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<InstructorApplicationDto>
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


    public async Task<InstructorApplicationDto?> GetByIdAsync(int id)
		{
			var entity = await _unitOfWork.GetRepository<InstructorApplication, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted); // ✅ SoftDelete filter

			return entity is null ? null : _mapper.Map<InstructorApplicationDto>(entity);
		}

		// ✅ Restore (زي ما اتفقنا)
		public async Task<bool> RestoreAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<InstructorApplication, int>();
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
