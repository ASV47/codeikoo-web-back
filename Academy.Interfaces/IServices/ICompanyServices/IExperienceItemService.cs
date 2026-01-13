using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IExperienceItemService
	{
		Task<IEnumerable<ExperienceItemDto>> GetAllAsync();
		Task<IEnumerable<ExperienceItemDto>> GetByCategoryAsync(int categoryId);
		Task<ExperienceItemDto?> GetByIdAsync(int id); // مضافة
		Task<ExperienceItemDto> CreateAsync(CreateExperienceItemDto dto);
		Task<bool> UpdateAsync(int id, CreateExperienceItemDto dto); // مضافة
		Task DeleteAsync(int id);
	}
}
