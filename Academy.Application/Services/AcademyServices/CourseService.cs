using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.IServices.IAcademyServices;
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
	public class CourseService : ICourseService
	{

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorage;
        private readonly ILocalizationService localization;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorage, ILocalizationService localization)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
            this.localization = localization;
        }

        public async Task<CourseDto> AddAsync(string userId, CreateCourseDto dto)
        {
            var imageUrl = string.Empty;

            if (dto.CourseImage is not null)
                imageUrl = await fileStorage.UploadAsync(dto.CourseImage, "CourseImages");

            
            var entity = mapper.Map<Course>(dto);
            entity.UserId = userId;
            entity.CourseImage = imageUrl;

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await unitOfWork.GetRepository<Course, int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<CourseDto> UpdateAsync(int id, string userId, CreateCourseDto dto)
        {
            var repo = unitOfWork.GetRepository<Course, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Course not found.");

            if (!string.Equals(entity.UserId, userId, StringComparison.Ordinal))
                throw new UnauthorizedAccessException("You are not allowed to update this course.");

            mapper.Map(dto, entity);

            if (dto.CourseImage is not null)
            {
                if (!string.IsNullOrWhiteSpace(entity.CourseImage))
                    await fileStorage.DeleteAsync(entity.CourseImage);

                entity.CourseImage = await fileStorage.UploadAsync(dto.CourseImage, "CourseImages");
            }


            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var entities = await unitOfWork.GetRepository<Course, int>()
                .Query()
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return entities.Select(ToDto).ToList();
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.GetRepository<Course, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (entity is null) throw new ArgumentException("Course not found.");

            return ToDto(entity);
        }

        public async Task<IEnumerable<CourseDto>> SearchAsync(string search)
        {
            search = (search ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(search))
                return new List<CourseDto>();

            var entities = await unitOfWork.GetRepository<Course, int>()
                .Query()
                .AsNoTracking()
                .Where(c => !c.IsDeleted &&
                    (
                        c.TilteArabic.Contains(search) ||
                        c.TitleEnglish.Contains(search) ||
                        (c.DescriptionAr != null && c.DescriptionAr.Contains(search)) ||
                        (c.DescriptionEn != null && c.DescriptionEn.Contains(search))
                    ))
                .OrderByDescending(c => c.Id)
                .Take(50)
                .ToListAsync();

            return entities.Select(ToDto).ToList();
        }

        public async Task DeleteAsync(int id)
        {
            var repo = unitOfWork.GetRepository<Course, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Course not found.");

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var repo = unitOfWork.GetRepository<Course, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null) throw new ArgumentException("Course not found.");
            if (!entity.IsDeleted) return;

            entity.IsDeleted = false;
            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        //private CourseDto ToDto(Course x)
        //{
        //    return new CourseDto
        //    {
        //        Id = x.Id,
        //        Title = localization.GetLocalizedTitle(x),
        //        Description = localization.GetLocalizedDescription(x),
        //        Duration = x.Duration,
        //        CourseImageUrl = x.CourseImage, // لو عندك BaseUrl ابعته أربطه
        //        Features = localization.GetLocalizedList(x.FeaturesAr, x.FeaturesEn)
        //    };
        //}

        private CourseDto ToDto(Course x)
        {
            return new CourseDto
            {
                Id = x.Id,
                Title = localization.GetLocalizedTitle(x),
                Description = localization.GetLocalizedDescription(x),
                Duration = x.Duration,

                // ✅ من غير IConfiguration في السيرفس
                CourseImageUrl = UploadcareUrlHelpers.ResolveUrl(
                    x.CourseImage,
                    baseUrl: null,
                    isImage: true,
                    imageOperation: "/-/preview/"
                ),

                Features = localization.GetLocalizedList(x.FeaturesAr, x.FeaturesEn)
            };
        }



    }

}
