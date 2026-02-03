using Academy.Interfaces.DTOs;
using Academy.Interfaces.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IInstructorApplicationService
	{
		Task<InstructorApplicationDto> AddAsync(CreateInstructorApplicationDto dto);
		Task<bool> DeleteAsync(int id);
        Task<PagedResult<InstructorApplicationDto>> GetAllAsync(PaginationParams pagination);
        Task<InstructorApplicationDto?> GetByIdAsync(int id);
		Task<bool> RestoreAsync(int id);

	}
}
