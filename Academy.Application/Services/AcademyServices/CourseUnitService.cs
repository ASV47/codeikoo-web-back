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
	public class CourseUnitService : ICourseUnitService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILocalizationService localization;

        public CourseUnitService(IUnitOfWork unitOfWork, IMapper mapper, ILocalizationService localization)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.localization = localization;
        }

        public async Task<CourseUnitDto> AddAsync(CreateCourseUnitDto dto)
        {
            var course = await unitOfWork.GetRepository<Course, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == dto.CourseId && !c.IsDeleted);

            if (course is null) throw new ArgumentException("Course not found.");

            var entity = mapper.Map<CourseUnit>(dto);

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await unitOfWork.GetRepository<CourseUnit, int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<CourseUnitDto> UpdateAsync(int id, CreateCourseUnitDto dto)
        {
            var repo = unitOfWork.GetRepository<CourseUnit, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Course unit not found.");

            // ✅ لو المستخدم غير CourseId، نتأكد إن الكورس موجود
            if (entity.CourseId != dto.CourseId)
            {
                var courseExists = await unitOfWork.GetRepository<Course, int>()
                    .Query()
                    .AsNoTracking()
                    .AnyAsync(c => c.Id == dto.CourseId && !c.IsDeleted);

                if (!courseExists)
                    throw new ArgumentException("Course not found.");
            }

            // ✅ تحديث البيانات (TitleArabic/TitleEnglish/CourseId ... حسب الـ mapping عندك)
            mapper.Map(dto, entity);

            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return ToDto(entity);
        }


        public async Task<List<CourseUnitDto>> GetAllAsync(int? courseId = null)
        {
            var query = unitOfWork.GetRepository<CourseUnit, int>()
                .Query()
                .AsNoTracking()
                .Where(u => !u.IsDeleted);

            if (courseId.HasValue)
                query = query.Where(u => u.CourseId == courseId.Value);

            var entities = await query
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return entities.Select(ToDto).ToList();
        }

        public async Task<CourseUnitDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.GetRepository<CourseUnit, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity is null) throw new ArgumentException("Course unit not found.");

            return ToDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var repo = unitOfWork.GetRepository<CourseUnit, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Course unit not found.");

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var repo = unitOfWork.GetRepository<CourseUnit, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null) throw new ArgumentException("Course unit not found.");
            if (!entity.IsDeleted) return;

            entity.IsDeleted = false;
            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        private CourseUnitDto ToDto(CourseUnit x)
        {
            return new CourseUnitDto
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = localization.GetLocalizedTitle(x)
            };
        }
    }
}
