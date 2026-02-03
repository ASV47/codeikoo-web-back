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
	public class CompanyContactMessageService(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyContactMessageService
	{
		public async Task SubmitMessageAsync(CreateCompanyContactMessageDTO dto)
		{
			if (!dto.termsAccepted)
				throw new ArgumentException("You must accept the terms.");

			var message = mapper.Map<CompanyContactMessage>(dto);
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			await repo.AddAsync(message);
			await unitOfWork.SaveChangesAsync();
		}

        // 1. جلب كل الرسائل (للوحة التحكم)
        public async Task<PagedResult<CompanyContactMessageDTO>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = unitOfWork.GetRepository<CompanyContactMessage, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)          // لو CompanyContactMessage فيه IsDeleted (زي باقي جداولك)
                .OrderByDescending(x => x.Id);     // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = mapper.Map<List<CompanyContactMessageDTO>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<CompanyContactMessageDTO>
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

        // 2. جلب رسالة محددة بالتفصيل
        public async Task<CompanyContactMessageDTO?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			var message = await repo.GetByIdAsync(id);
			return mapper.Map<CompanyContactMessageDTO>(message);
		}

		// 3. حذف رسالة
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			var message = await repo.GetByIdAsync(id);
			if (message == null) return false;

			repo.Delete(message);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
