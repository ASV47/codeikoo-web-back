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
	public class ExperienceService(IUnitOfWork unitOfWork, IMapper mapper) : IExperienceService
	{
		public async Task<IEnumerable<ExperienceDto>> GetAsync()
		{
			var repo = unitOfWork.GetRepository<Experience, int>();
			var all = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<ExperienceDto>>(all);
		}
		public async Task<ExperienceDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Experience, int>();
			var entity = await repo.GetByIdAsync(id);
			return mapper.Map<ExperienceDto>(entity);
		}
		public async Task<ExperienceDto> CreateAsync(CreateExperienceDto dto)
		{
			var repo = unitOfWork.GetRepository<Experience, int>();

			// التأكد من عدم وجود سجل سابق (لأن التصميم يتطلب عنوان واحد فقط)
			var exists = await repo.GetAllAsync();
			if (exists.Any())
				throw new InvalidOperationException("Experience record already exists.");

			var entity = mapper.Map<Experience>(dto);

			await repo.AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<ExperienceDto>(entity);
		}

		public async Task<ExperienceDto> UpdateAsync(int id, CreateExperienceDto dto)
		{
			var repo = unitOfWork.GetRepository<Experience, int>();
			var entity = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Experience not found.");

			// تحديث البيانات من الـ DTO إلى الـ Entity
			mapper.Map(dto, entity);

			repo.Update(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<ExperienceDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Experience, int>();
			var entity = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Experience not found.");

			repo.Delete(entity);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
