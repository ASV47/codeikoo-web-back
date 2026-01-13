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
	public class ServiceHandler(IUnitOfWork unitOfWork, IMapper mapper) : IServiceHandler
	{
		public async Task<IEnumerable<ServiceDto>> GetAllAsync()
		{
			var services = await unitOfWork.GetRepository<Service, int>().GetAllAsync();
			return mapper.Map<IEnumerable<ServiceDto>>(services);
		}
		public async Task<bool> UpdateAsync(int id, CreateServiceDTO serviceDTO)
		{
			var repo = unitOfWork.GetRepository<Service, int>();
			var service = await repo.GetByIdAsync(id);

			if (service == null) return false;

			service.Title = serviceDTO.Title;
			service.Description = serviceDTO.Description;

			// تحديث الأيقونة فقط إذا تم رفع واحدة جديدة
			if (!string.IsNullOrEmpty(serviceDTO.IconUrl))
			{
				service.IconUrl = serviceDTO.IconUrl;
			}

			repo.Update(service);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
		public async Task AddAsync(CreateServiceDTO serviceDTO)
		{
			await unitOfWork.GetRepository<Service, int>().AddAsync(new Service { Title = serviceDTO.Title, Description = serviceDTO.Description, IconUrl = serviceDTO.IconUrl });
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Service, int>();
			var service = await repo.GetByIdAsync(id);

			if (service == null) return false;

			repo.Delete(service);
			var result = await unitOfWork.SaveChangesAsync();

			return result > 0;
		}

		public async Task<ServiceDto?> GetByIdAsync(int id)
		{
			var service = await unitOfWork.GetRepository<Service, int>().GetByIdAsync(id);
			return mapper.Map<ServiceDto>(service);
		}
	}
}
