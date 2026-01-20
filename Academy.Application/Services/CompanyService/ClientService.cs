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
using Microsoft.AspNetCore.Http;
using Academy.Interfaces.IServices.IAcademyServices;

namespace ServiceLayer
{
	public class ClientService(IUnitOfWork unitOfWork,
        IMapper mapper, IFileStorageService fileStorage) : IClientService
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

        public async Task AddAsync(IFormFile? image)
        {
            string logoUrl = string.Empty;

            if (image is not null)
                logoUrl = await fileStorage.UploadAsync(image, "ClientsLogo");

            await unitOfWork.GetRepository<Client, int>()
                .AddAsync(new Client { LogoUrl = logoUrl });

            await unitOfWork.SaveChangesAsync();
        }


        public async Task<bool> UpdateAsync(int id, IFormFile? image)
        {
            var repo = unitOfWork.GetRepository<Client, int>();
            var client = await repo.GetByIdAsync(id);
            if (client == null) return false;

            if (image is not null)
            {
                // امسح القديم من Uploadcare لو موجود
                if (!string.IsNullOrWhiteSpace(client.LogoUrl))
                    await fileStorage.DeleteAsync(client.LogoUrl);

                // ارفع الجديد
                client.LogoUrl = await fileStorage.UploadAsync(image, "ClientsLogo");
            }

            repo.Update(client);
            return await unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var repo = unitOfWork.GetRepository<Client, int>();
            var client = await repo.GetByIdAsync(id);
            if (client == null) return false;

            // احذف اللوجو من Uploadcare
            if (!string.IsNullOrWhiteSpace(client.LogoUrl))
                await fileStorage.DeleteAsync(client.LogoUrl);

            repo.Delete(client);
            return await unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
