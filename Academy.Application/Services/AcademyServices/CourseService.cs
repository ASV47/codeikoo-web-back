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
		//public async Task<CourseDto> AddAsync(string userId, CreateCourseDto dto, string? lang = "en")
		//{
		//	string imagePath = string.Empty;
		//	if (dto.CourseImage is not null)
		//		imagePath = DocumentSettings.UploadFile(dto.CourseImage, "CourseImages");

		//	var entity = mapper.Map<Course>(dto);
		//	entity.UserId = userId;
		//	entity.CourseImage = imagePath;

		//	AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

		//	await unitOfWork.GetRepository<Course, int>().AddAsync(entity);
		//	await unitOfWork.SaveChangesAsync();

		//	return mapper.Map<CourseDto>(entity, opt => opt.Items["lang"] = lang);
		//}

		public async Task<CourseDto> AddAsync(string userId, CreateCourseDto dto, string? lang = "en")
		{
			string imagePath = string.Empty;
			if (dto.CourseImage is not null)
				imagePath = DocumentSettings.UploadFile(dto.CourseImage, "CourseImages");

			var entity = mapper.Map<Course>(dto);
			entity.UserId = userId;
			entity.CourseImage = imagePath;

			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await unitOfWork.GetRepository<Course, int>().AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<CourseDto>(entity, opt => opt.Items["lang"] = lang);
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


		public async Task<CourseDto> UpdateAsync(int id, string userId, CreateCourseDto dto, string? lang = "en")
		{
			var repo = unitOfWork.GetRepository<Course, int>();

			var entity = await repo.GetByIdAsync(id);
			if (entity is null || entity.IsDeleted)
				throw new ArgumentException("Course not found.");

			// ✅ لو عايز تمنع تعديل كورس مش بتاع المستخدم
			if (!string.Equals(entity.UserId, userId, StringComparison.Ordinal))
				throw new UnauthorizedAccessException("You are not allowed to update this course.");

			mapper.Map(dto, entity);

			// ✅ الصورة: لو المستخدم بعت صورة جديدة
			if (dto.CourseImage is not null)
			{
				if (!string.IsNullOrWhiteSpace(entity.CourseImage))
					DocumentSettings.DeleteFile(entity.CourseImage);

				entity.CourseImage = DocumentSettings.UploadFile(dto.CourseImage, "CourseImages");
			}
			// ✅ لو مبعتش صورة جديدة: سيب القديمة زي ما هي

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
