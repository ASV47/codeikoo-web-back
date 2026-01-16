using Academy.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IUnitLessonService
	{
		Task<UnitLessonDto> AddAsync(CreateUnitLessonDto dto, string? lang = "en");

		Task<List<UnitLessonDto>> GetAllAsync(int? courseUnitId = null, string? lang = "en");
		Task<UnitLessonDto> GetByIdAsync(int id, string? lang = "en");

		Task<UnitLessonDto> UpdateAsync(int id, CreateUnitLessonDto dto, string? lang = "en");
		Task DeleteAsync(int id);
		Task RestoreAsync(int id);
	}
}
