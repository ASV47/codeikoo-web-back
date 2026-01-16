using Academy.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface ICourseUnitService
	{
		Task<CourseUnitDto> AddAsync(CreateCourseUnitDto dto, string? lang = "en");
		Task<List<CourseUnitDto>> GetAllAsync(int? courseId = null, string? lang = "en");
		Task<CourseUnitDto> GetByIdAsync(int id, string? lang = "en");
		Task DeleteAsync(int id);
		Task RestoreAsync(int id);
	}
}
