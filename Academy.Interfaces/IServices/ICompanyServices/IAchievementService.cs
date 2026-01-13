using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IAchievementService
	{
		Task<IEnumerable<AchievementDto>> GetAllAsync();
		Task<AchievementDto?> GetByIdAsync(int id); // مضافة
		Task<AchievementDto> AddAsync(CreateAchievementDTO dto); // مضافة
		Task<bool> UpdateAsync(int id, CreateAchievementDTO dto);
		Task<bool> DeleteAsync(int id);
	}
}
