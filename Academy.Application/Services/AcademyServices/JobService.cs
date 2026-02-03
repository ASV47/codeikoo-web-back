using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class JobService : IJobService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localization;

        public JobService(IUnitOfWork unitOfWork, IMapper mapper, ILocalizationService localization)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = localization;
        }

        public async Task<JobDto> AddAsync(CreateJobDto dto)
        {
            var entity = _mapper.Map<Job>(dto);
            entity.PostedAt = dto.PostedAt ?? DateTime.UtcNow;

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await _unitOfWork.GetRepository<Job, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<JobDto> UpdateAsync(int id, CreateJobDto dto)
        {
            var repo = _unitOfWork.GetRepository<Job, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Job not found.");

            // Update fields via AutoMapper
            _mapper.Map(dto, entity);

            // لو PostedAt nullable في CreateJobDto وعايز تحافظ على القديم لو null:
            if (dto.PostedAt.HasValue)
                entity.PostedAt = dto.PostedAt.Value;

            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(entity); // نفس اللي عندك في GetAll
        }


        public async Task<PagedResult<JobDto>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = _unitOfWork.GetRepository<Job, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id); // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var jobs = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = jobs.Select(ToDto).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<JobDto>
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

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<Job, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return entity is null ? null : ToDto(entity);
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

        private JobDto ToDto(Job x)
        {
            return new JobDto
            {
                Id = x.Id,
                Title = _localization.GetLocalizedTitle(x),
                Description = _localization.GetLocalizedDescription(x),
                Location = x.Location,
                EmploymentType = x.EmploymentType,
                PostedAt = x.PostedAt,
                Requirements = _localization.GetLocalizedList(x.RequirementsAr, x.RequirementsEn)
            };
        }
    }

}
