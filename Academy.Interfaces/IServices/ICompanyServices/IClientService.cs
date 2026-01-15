using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IClientService
	{
		Task<IEnumerable<ClientDto>> GetAllAsync();
		Task<ClientDto?> GetByIdAsync(int id);
		Task AddAsync(string logoUrl);
		Task<bool> DeleteAsync(int id);
		Task<bool> UpdateAsync(int id, string logoUrl);
	}
}
