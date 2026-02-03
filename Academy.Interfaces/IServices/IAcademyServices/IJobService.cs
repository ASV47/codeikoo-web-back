using Academy.Interfaces.DTOs;
using Academy.Interfaces.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IJobService
	{
        Task<JobDto> AddAsync(CreateJobDto dto);
        Task<bool> DeleteAsync(int id);
        Task<JobDto> UpdateAsync(int id, CreateJobDto dto);
        Task<PagedResult<JobDto>> GetAllAsync(PaginationParams pagination);
        Task<JobDto?> GetByIdAsync(int id);
        Task<bool> RestoreAsync(int id);

    }
}
