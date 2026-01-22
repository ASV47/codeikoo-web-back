using Microsoft.AspNetCore.Http;
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
		Task<FlexibilityItemDto?> GetByIdAsync(int id);
        Task AddAsync(string title, IFormFile? icon);
        Task<bool> UpdateAsync(int id, string title, IFormFile? icon);
        Task<bool> DeleteAsync(int id);
    }
}
