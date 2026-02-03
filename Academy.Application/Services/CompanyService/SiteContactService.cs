using AbstractionLayer;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.Pagination;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<PagedResult<SiteContactDto>> GetAllContactsAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = unitOfWork.GetRepository<SiteContact, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)      // لو SiteContact فيه SoftDelete
                .OrderByDescending(x => x.Id); // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = mapper.Map<List<SiteContactDto>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<SiteContactDto>
            {
                Items = items,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = pagination.PageNumber > 1,
                HasNextPage = pagination.PageNumber < totalPages
            };
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
