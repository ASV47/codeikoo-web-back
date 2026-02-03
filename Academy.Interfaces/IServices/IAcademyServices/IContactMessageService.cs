using Academy.Interfaces.DTOs;
using Academy.Interfaces.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IContactMessageService
	{
		Task<ContactMessageDto> AddAsync(CreateContactMessageDto dto);
		Task<bool> DeleteAsync(int id);
        Task<PagedResult<ContactMessageDto>> GetAllAsync(PaginationParams pagination);
        Task<ContactMessageDto?> GetByIdAsync(int id);
		Task<bool> RestoreAsync(int id);

	}
}
