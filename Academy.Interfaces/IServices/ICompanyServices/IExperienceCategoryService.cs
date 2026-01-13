using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IExperienceCategoryService
	{
		Task<IEnumerable<ExperienceCategoryDto>> GetAllAsync();
		Task<ExperienceCategoryDto?> GetByIdAsync(int id); // مضافة
		Task<ExperienceCategoryDto> CreateAsync(CreateExperienceCategoryDto dto);
		Task<bool> UpdateAsync(int id, CreateExperienceCategoryDto dto); // مضافة
		Task DeleteAsync(int id);
	}
}
