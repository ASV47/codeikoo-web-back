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
        Task<UnitLessonDto> AddAsync(CreateUnitLessonDto dto);
        Task<List<UnitLessonDto>> GetAllAsync(int? courseUnitId = null);
        Task<UnitLessonDto> GetByIdAsync(int id);
        Task<UnitLessonDto> UpdateAsync(int id, CreateUnitLessonDto dto);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
    }
}
