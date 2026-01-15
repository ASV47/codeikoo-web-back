using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface ITechnologyService
	{
		Task<IEnumerable<TechnologyDto>> GetAllAsync();
		Task AddAsync(string imageUrl);
		Task<bool> UpdateAsync(int id, string imageUrl);
		Task<bool> DeleteAsync(int id);
		Task<TechnologyDto?> GetByIdAsync(int id);
	}
}
