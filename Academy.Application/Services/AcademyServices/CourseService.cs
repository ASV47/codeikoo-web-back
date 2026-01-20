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
	public class CourseService(IUnitOfWork unitOfWork, IMapper mapper,
        IFileStorageService fileStorage) : ICourseService
	{

        public async Task<CourseDto> AddAsync(string userId, CreateCourseDto dto, string? lang = "en")
        {
            string imageUrl = string.Empty;

            if (dto.CourseImage is not null)
                imageUrl = await fileStorage.UploadAsync(dto.CourseImage, "CourseImages");

            var entity = mapper.Map<Course>(dto);
            entity.UserId = userId;
            entity.CourseImage = imageUrl;

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await unitOfWork.GetRepository<Course, int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<CourseDto>(entity, opt => opt.Items["lang"] = lang);
        }

        public async Task<CourseDto> UpdateAsync(int id, string userId, CreateCourseDto dto, string? lang = "en")
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
                // اختياري: امسح القديمة من Uploadcare
                if (!string.IsNullOrWhiteSpace(entity.CourseImage))
                    await fileStorage.DeleteAsync(entity.CourseImage);

                entity.CourseImage = await fileStorage.UploadAsync(dto.CourseImage, "CourseImages");
            }

            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<CourseDto>(entity, opt => opt.Items["lang"] = lang);
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




        public async Task<IEnumerable<CourseDto>> GetAllAsync(string? lang = "en")
		{
			var entities = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.Where(c => !c.IsDeleted)
				.OrderByDescending(c => c.Id)
				.ToListAsync();

			return mapper.Map<List<CourseDto>>(entities, opt => opt.Items["lang"] = lang);
		}


		public async Task<CourseDto> GetByIdAsync(int id, string? lang = "en")
		{
			var entity = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

			if (entity is null) throw new ArgumentException("Course not found.");

			return mapper.Map<CourseDto>(entity, opt => opt.Items["lang"] = lang);
		}


		public async Task<IEnumerable<CourseDto>> SearchAsync(string search, string? lang = "en")
		{
			search = (search ?? string.Empty).Trim();
			if (string.IsNullOrWhiteSpace(search))
				return new List<CourseDto>();

			var entities = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.Where(c => !c.IsDeleted &&
						   (c.Title.Ar.Contains(search) ||
							c.Title.En.Contains(search) ||
							c.Description.Ar.Contains(search) ||
							c.Description.En.Contains(search)))
				.OrderByDescending(c => c.Id)
				.Take(50)
				.ToListAsync();

			return mapper.Map<List<CourseDto>>(entities, opt => opt.Items["lang"] = lang);
		}

		public async Task RestoreAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Course, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity is null) throw new ArgumentException("Course not found.");

			if (!entity.IsDeleted) return; // already active

			entity.IsDeleted = false;
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();
		}


	}

}
