using Academy.Interfaces.DTOs;
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
		Task<IEnumerable<ContactMessageDto>> GetAllAsync();
		Task<ContactMessageDto?> GetByIdAsync(int id);
	}
}
