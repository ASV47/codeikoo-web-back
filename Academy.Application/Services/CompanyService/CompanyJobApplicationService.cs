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
	public class CompanyJobApplicationService(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyJobApplicationService
	{
		public async Task SubmitApplicationAsync(CreateCompanyJopApplicationDTO dto)
		{
			try
			{
				if (!dto.termsAccepted)
					throw new ArgumentException("You must accept the terms.");

				var application = mapper.Map<CompanyJopApplication>(dto);
				var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
				await repo.AddAsync(application);
				await unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Log ex.Message
				throw; // أو return BadRequest(ex.Message)
			}
		}

        // 1. جلب كل طلبات التوظيف
        public async Task<PagedResult<CompanyJopApplicationDTO>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var query = unitOfWork.GetRepository<CompanyJopApplication, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)      // لو CompanyJopApplication فيه IsDeleted
                .OrderByDescending(x => x.Id); // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = mapper.Map<List<CompanyJopApplicationDTO>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<CompanyJopApplicationDTO>
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

        // 2. جلب طلب توظيف معين بالتفصيل
        public async Task<CompanyJopApplicationDTO?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
			var application = await repo.GetByIdAsync(id);
			return mapper.Map<CompanyJopApplicationDTO>(application);
		}

		// 3. حذف طلب توظيف
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
			var application = await repo.GetByIdAsync(id);
			if (application == null) return false;

			repo.Delete(application);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
