using AbstractionLayer;
using AutoMapper;
using CoreLayer.Entities;
using Academy.Interfaces.Interfaces;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ExperienceItemService(IUnitOfWork unitOfWork, IMapper mapper) : IExperienceItemService
	{
		public async Task<IEnumerable<ExperienceItemDto>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			var entities = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<ExperienceItemDto>>(entities);
		}

		public async Task<IEnumerable<ExperienceItemDto>> GetByCategoryAsync(int categoryId)
		{
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			// ملاحظة: الـ Generic Repository غالباً يحتاج ميثود تدعم الـ Specification أو التحميل المسبق (Include)
			// إذا كان الـ GetAll يرجع IQueryable سيكون الأداء أفضل
			var entities = await repo.GetAllAsync();
			var filtered = entities.Where(e => e.ExperienceCategoryId == categoryId);
			return mapper.Map<IEnumerable<ExperienceItemDto>>(filtered);
		}

		// 1. GetByIdAsync
		public async Task<ExperienceItemDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			var entity = await repo.GetByIdAsync(id);
			return mapper.Map<ExperienceItemDto>(entity);
		}

		public async Task<ExperienceItemDto> CreateAsync(CreateExperienceItemDto dto)
		{
			var entity = mapper.Map<ExperienceItem>(dto);
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			await repo.AddAsync(entity);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<ExperienceItemDto>(entity);
		}

		// 2. UpdateAsync
		public async Task<bool> UpdateAsync(int id, CreateExperienceItemDto dto)
		{
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity == null) return false;

			// تحديث الاسم وربطه بالتصنيف الجديد إذا تغير
			mapper.Map(dto, entity);

			repo.Update(entity);
			return await unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<ExperienceItem, int>();
			var entity = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Item not found.");
			repo.Delete(entity);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
