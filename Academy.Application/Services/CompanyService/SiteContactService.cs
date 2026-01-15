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
	public class SiteContactService(IUnitOfWork unitOfWork, IMapper mapper) : ISiteContactService
	{
		public async Task<IEnumerable<SiteContactDto>> GetAllContactsAsync()
		{
			var repo = unitOfWork.GetRepository<SiteContact, int>();
			var contacts = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<SiteContactDto>>(contacts);
		}

		public async Task<SiteContactDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<SiteContact, int>();
			var contact = await repo.GetByIdAsync(id);
			return mapper.Map<SiteContactDto>(contact);
		}

		public async Task<SiteContactDto> CreateContactAsync(CreateSiteContactDto dto)
		{
			var repo = unitOfWork.GetRepository<SiteContact, int>();

			var contact = mapper.Map<SiteContact>(dto);

			await repo.AddAsync(contact);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<SiteContactDto>(contact);
		}

		public async Task<SiteContactDto> UpdateContactAsync(int id, CreateSiteContactDto dto)
		{
			var repo = unitOfWork.GetRepository<SiteContact, int>();

			var contact = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Contact not found.");

			mapper.Map(dto, contact);

			repo.Update(contact);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<SiteContactDto>(contact);
		}

		public async Task DeleteContactAsync(int id)
		{
			var repo = unitOfWork.GetRepository<SiteContact, int>();
			var contact = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Contact not found.");
			repo.Delete(contact);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
