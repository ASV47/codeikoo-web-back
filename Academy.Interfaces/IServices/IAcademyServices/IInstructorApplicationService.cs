using Academy.Interfaces.DTOs;
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
		Task<IEnumerable<InstructorApplicationDto>> GetAllAsync();
		Task<InstructorApplicationDto?> GetByIdAsync(int id);
	}
}
