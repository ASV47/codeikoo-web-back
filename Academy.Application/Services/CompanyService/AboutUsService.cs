using AbstractionLayer;
using Academy.Interfaces.Interfaces;
using AutoMapper;
using CoreLayer.Entities;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class AboutUsService(IUnitOfWork unitOfWork, IMapper mapper) : IAboutUsService
	{
		public async Task<IEnumerable<AboutUsDto>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<AboutUs, int>();
			var data = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<AboutUsDto>>(data);
		}

		// 1. GetByIdAsync
		public async Task<AboutUsDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<AboutUs, int>();
			var entity = await repo.GetByIdAsync(id);
			return mapper.Map<AboutUsDto>(entity);
		}

		// 2. AddAsync
		public async Task<AboutUsDto> AddAsync(CreateAboutUsDto dto)
		{
			var repo = unitOfWork.GetRepository<AboutUs, int>();
			var entity = mapper.Map<AboutUs>(dto);

			await repo.AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<AboutUsDto>(entity);
		}

		public async Task<bool> UpdateAsync(int id, CreateAboutUsDto dto)
		{
			var repo = unitOfWork.GetRepository<AboutUs, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity == null) return false;

			entity.Title = dto.Title;
			entity.Description = dto.Description;

			repo.Update(entity);
			return await unitOfWork.SaveChangesAsync() > 0;
		}

		// 3. DeleteAsync
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<AboutUs, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity == null) return false;

			repo.Delete(entity);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
