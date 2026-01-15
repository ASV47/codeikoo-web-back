using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IServiceHandler
	{
		Task<IEnumerable<ServiceDto>> GetAllAsync();
		Task AddAsync(CreateServiceDTO serviceDTO);
		Task<bool> UpdateAsync(int id, CreateServiceDTO serviceDTO); // مضافة
		Task<bool> DeleteAsync(int id);
		Task<ServiceDto?> GetByIdAsync(int id);
	}
}
