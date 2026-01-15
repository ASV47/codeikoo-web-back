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
	public class UnitLessonService(IUnitOfWork unitOfWork, IMapper mapper) : IUnitLessonService
	{
		public async Task<UnitLessonDto> AddAsync(CreateUnitLessonDto dto)
		{
			// ✅ CourseUnit لازم يكون موجود ومش SoftDeleted
			var unit = await unitOfWork.GetRepository<CourseUnit, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id == dto.CourseUnitId && !u.IsDeleted);

			if (unit is null) throw new ArgumentException("Course unit not found.");

			var entity = mapper.Map<UnitLesson>(dto);

			// ✅ Audit (Created)
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await unitOfWork.GetRepository<UnitLesson, int>().AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<UnitLessonDto>(entity);
		}

		public async Task<List<UnitLessonDto>> GetAllAsync(int? courseUnitId = null)
		{
			var query = unitOfWork.GetRepository<UnitLesson, int>()
				.Query()
				.AsNoTracking()
				.Where(l => !l.IsDeleted); // ✅ SoftDelete filter

			if (courseUnitId.HasValue)
				query = query.Where(l => l.CourseUnitId == courseUnitId.Value);

			var entities = await query
				.OrderByDescending(l => l.Id)
				.ToListAsync();

			return mapper.Map<List<UnitLessonDto>>(entities);
		}

		public async Task<UnitLessonDto> GetByIdAsync(int id)
		{
			var entity = await unitOfWork.GetRepository<UnitLesson, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted); // ✅ SoftDelete filter

			if (entity is null) throw new ArgumentException("Lesson not found.");
			return mapper.Map<UnitLessonDto>(entity);
		}

		public async Task<UnitLessonDto> UpdateAsync(int id, CreateUnitLessonDto dto)
		{
			var repo = unitOfWork.GetRepository<UnitLesson, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null || entity.IsDeleted) throw new ArgumentException("Lesson not found.");

			mapper.Map(dto, entity);

			// ✅ Audit (Modified)
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<UnitLessonDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<UnitLesson, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null || entity.IsDeleted) throw new ArgumentException("Lesson not found.");

			// ✅ SoftDelete
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();
		}

		// ✅ Restore
		public async Task RestoreAsync(int id)
		{
			var repo = unitOfWork.GetRepository<UnitLesson, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) throw new ArgumentException("Lesson not found.");
			if (!entity.IsDeleted) return;

			entity.IsDeleted = false;
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();
		}
	}

}
