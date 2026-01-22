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

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _unitOfWork.GetRepository<Job, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return jobs.Select(ToDto).ToList();
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
