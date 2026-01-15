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
    public class WebSettingsService(IUnitOfWork unitOfWork, IMapper mapper): IWebSettingsService
    {
		public async Task<IEnumerable<WebSettingsDto>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<WebSettings, int>();
			var entities = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<WebSettingsDto>>(entities);
		}

		public async Task<WebSettingsDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<WebSettings, int>();
			var entity = await repo.GetByIdAsync(id);
			return mapper.Map<WebSettingsDto>(entity);
		}

		public async Task<WebSettingsDto> CreateAsync(CreateWebSettingsDto dto)
		{
			var entity = mapper.Map<WebSettings>(dto);
			var repo = unitOfWork.GetRepository<WebSettings, int>();

			await repo.AddAsync(entity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<WebSettingsDto>(entity);
		}

		public async Task<WebSettingsDto> UpdateAsync(int id, CreateWebSettingsDto dto)
		{
			var repo = unitOfWork.GetRepository<WebSettings, int>();
			var existingEntity = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Settings not found.");

			// تحديث القيم الموجودة بالقيم الجديدة من الـ DTO
			mapper.Map(dto, existingEntity);

			repo.Update(existingEntity);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<WebSettingsDto>(existingEntity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<WebSettings, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity == null) return false;

			repo.Delete(entity);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
