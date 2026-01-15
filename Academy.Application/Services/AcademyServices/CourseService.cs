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
	public class CourseService(IUnitOfWork unitOfWork, IMapper mapper) : ICourseService
	{
		public async Task<CourseDto> AddAsync(string userId, CreateCourseDto dto)
		{
			string imagePath = string.Empty;
			if (dto.CourseImage is not null)
				imagePath = DocumentSettings.UploadFile(dto.CourseImage, "CourseImages");

			var entity = mapper.Map<Course>(dto);
			entity.UserId = userId;
			entity.CourseImage = imagePath;

			// ✅ Audit (Created)
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await unitOfWork.GetRepository<Course, int>().AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<CourseDto>(entity);
		}

		public async Task<IEnumerable<CourseDto>> GetAllAsync()
		{
			var entities = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.Where(c => !c.IsDeleted)                 // ✅ SoftDelete filter
				.OrderByDescending(c => c.Id)
				.ToListAsync();

			return mapper.Map<List<CourseDto>>(entities);
		}

		public async Task<CourseDto> GetByIdAsync(int id)
		{
			var entity = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);   // ✅ SoftDelete filter

			if (entity is null) throw new ArgumentException("Course not found.");
			return mapper.Map<CourseDto>(entity);
		}

		public async Task<IEnumerable<CourseDto>> SearchAsync(string search)
		{
			search = (search ?? string.Empty).Trim();
			if (string.IsNullOrWhiteSpace(search))
				return new List<CourseDto>();

			var entities = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.Where(c => !c.IsDeleted &&                               // ✅ SoftDelete filter
						   (c.Title.Contains(search) ||
							c.Description.Contains(search)))
				.OrderByDescending(c => c.Id)
				.Take(50)
				.ToListAsync();

			return mapper.Map<List<CourseDto>>(entities);
		}

		public async Task<CourseDto> UpdateAsync(int id, CreateCourseDto dto)
		{
			var repo = unitOfWork.GetRepository<Course, int>();

			// خليه tracked عشان نقدر نعدل ونحفظ + audit
			var entity = await repo.GetByIdAsync(id);
			if (entity is null || entity.IsDeleted) throw new ArgumentException("Course not found.");

			mapper.Map(dto, entity);

			if (dto.CourseImage is not null)
			{
				if (!string.IsNullOrWhiteSpace(entity.CourseImage))
					DocumentSettings.DeleteFile(entity.CourseImage);

				entity.CourseImage = DocumentSettings.UploadFile(dto.CourseImage, "CourseImages");
			}

			// ✅ Audit (Modified)
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<CourseDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Course, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity is null || entity.IsDeleted)
				throw new ArgumentException("Course not found.");

			// ✅ SoftDelete فقط (من غير حذف الصورة أو أي شيء)
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();
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
