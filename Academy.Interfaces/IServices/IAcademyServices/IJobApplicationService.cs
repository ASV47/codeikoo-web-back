using Academy.Interfaces.DTOs;
using Academy.Interfaces.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IJobApplicationService
	{
		Task<JobApplicationDto> AddAsync(CreateJobApplicationDto dto);
		Task<bool> DeleteAsync(int id);
        Task<PagedResult<JobApplicationDto>> GetAllAsync(PaginationParams pagination);
        Task<JobApplicationDto?> GetByIdAsync(int id);
		Task<bool> RestoreAsync(int id);

	}
}
