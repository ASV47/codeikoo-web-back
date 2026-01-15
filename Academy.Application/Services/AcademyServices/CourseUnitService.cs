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
	public class CourseUnitService(IUnitOfWork unitOfWork, IMapper mapper) : ICourseUnitService
	{
		public async Task<CourseUnitDto> AddAsync(CreateCourseUnitDto dto)
		{
			// ✅ اتأكد إن الكورس موجود ومش SoftDeleted
			var course = await unitOfWork.GetRepository<Course, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == dto.CourseId && !c.IsDeleted);

			if (course is null) throw new ArgumentException("Course not found.");

			var entity = mapper.Map<CourseUnit>(dto);

			// ✅ Audit (Created)
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await unitOfWork.GetRepository<CourseUnit, int>().AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<CourseUnitDto>(entity);
		}

		public async Task<List<CourseUnitDto>> GetAllAsync(int? courseId = null)
		{
			var query = unitOfWork.GetRepository<CourseUnit, int>()
				.Query()
				.AsNoTracking()
				.Where(u => !u.IsDeleted); // ✅ SoftDelete filter

			if (courseId.HasValue)
				query = query.Where(u => u.CourseId == courseId.Value);

			var entities = await query
				.OrderByDescending(u => u.Id)
				.ToListAsync();

			return mapper.Map<List<CourseUnitDto>>(entities);
		}

		public async Task<CourseUnitDto> GetByIdAsync(int id)
		{
			var entity = await unitOfWork.GetRepository<CourseUnit, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted); // ✅ SoftDelete filter

			if (entity is null) throw new ArgumentException("Course unit not found.");
			return mapper.Map<CourseUnitDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CourseUnit, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity is null || entity.IsDeleted) throw new ArgumentException("Course unit not found.");

			// ✅ SoftDelete (بدون حذف)
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();
		}

		// ✅ Restore (زي ما اتفقنا)
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
	}

}
