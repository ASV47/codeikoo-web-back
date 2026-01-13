using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IFlexibilityItemService
	{
		Task<IEnumerable<FlexibilityItemDto>> GetAllAsync();
		Task<bool> UpdateAsync(int id, string title, string? iconUrl);
		Task AddAsync(string title, string iconUrl);
		Task<FlexibilityItemDto?> GetByIdAsync(int id);
		Task<bool> DeleteAsync(int id);
	}
}
