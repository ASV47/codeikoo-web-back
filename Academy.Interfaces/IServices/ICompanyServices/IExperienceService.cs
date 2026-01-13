using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IExperienceService
	{
		Task<IEnumerable<ExperienceDto>> GetAsync();
		Task<ExperienceDto> CreateAsync(CreateExperienceDto dto); // تم التعديل هنا
		Task<ExperienceDto> UpdateAsync(int id, CreateExperienceDto dto); // تم التعديل هنا
		Task<ExperienceDto?> GetByIdAsync(int id);
		Task DeleteAsync(int id);
	}
}
