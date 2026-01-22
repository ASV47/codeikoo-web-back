using Microsoft.AspNetCore.Http;
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
		Task AddAsync(IFormFile? image);
		Task<bool> UpdateAsync(int id, IFormFile? image);
		Task<bool> DeleteAsync(int id);
	}
}
