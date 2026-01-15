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
	public class ExperienceCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : IExperienceCategoryService
	{
		public async Task<IEnumerable<ExperienceCategoryDto>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<ExperienceCategory, int>();
			var entities = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<ExperienceCategoryDto>>(entities);
		}

		// 1. GetByIdAsync
		public async Task<ExperienceCategoryDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<ExperienceCategory, int>();
			var entity = await repo.GetByIdAsync(id);
			return mapper.Map<ExperienceCategoryDto>(entity);
		}

		public async Task<ExperienceCategoryDto> CreateAsync(CreateExperienceCategoryDto dto)
		{
			var entity = mapper.Map<ExperienceCategory>(dto);
			var repo = unitOfWork.GetRepository<ExperienceCategory, int>();
			await repo.AddAsync(entity);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<ExperienceCategoryDto>(entity);
		}

		// 2. UpdateAsync
		public async Task<bool> UpdateAsync(int id, CreateExperienceCategoryDto dto)
		{
			var repo = unitOfWork.GetRepository<ExperienceCategory, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity == null) return false;

			// تحديث البيانات باستخدام Mapper
			mapper.Map(dto, entity);

			repo.Update(entity);
			return await unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<ExperienceCategory, int>();
			var entity = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Category not found.");
			repo.Delete(entity);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
