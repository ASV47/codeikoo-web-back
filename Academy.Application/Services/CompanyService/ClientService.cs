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
	public class ClientService(IUnitOfWork unitOfWork, IMapper mapper) : IClientService
	{
		public async Task<IEnumerable<ClientDto>> GetAllAsync()
		{
			var clients = await unitOfWork.GetRepository<Client, int>().GetAllAsync();
			return mapper.Map<IEnumerable<ClientDto>>(clients);
		}

		public async Task<ClientDto?> GetByIdAsync(int id)
		{
			var client = await unitOfWork.GetRepository<Client, int>().GetByIdAsync(id);
			return mapper.Map<ClientDto>(client);
		}

		public async Task AddAsync(string logoUrl)
		{
			await unitOfWork.GetRepository<Client, int>().AddAsync(new Client { LogoUrl = logoUrl });
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> UpdateAsync(int id, string logoUrl)
		{
			var repo = unitOfWork.GetRepository<Client, int>();
			var client = await repo.GetByIdAsync(id);
			if (client == null) return false;

			// تحديث اللوجو فقط إذا تم إرسال مسار جديد
			if (!string.IsNullOrEmpty(logoUrl))
			{
				client.LogoUrl = logoUrl;
			}

			repo.Update(client);
			return await unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Client, int>();
			var client = await repo.GetByIdAsync(id);
			if (client == null) return false;

			repo.Delete(client);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
