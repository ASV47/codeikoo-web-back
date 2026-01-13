using Academy.Interfaces.DTOs;
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
		Task<IEnumerable<JobDto>> GetAllAsync();
		Task<JobDto?> GetByIdAsync(int id);
	}
}
