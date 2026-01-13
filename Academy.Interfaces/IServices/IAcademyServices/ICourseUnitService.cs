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
		Task<CourseUnitDto> AddAsync(CreateCourseUnitDto dto);
		Task<List<CourseUnitDto>> GetAllAsync(int? courseId = null);
		Task<CourseUnitDto> GetByIdAsync(int id);
		Task DeleteAsync(int id);
	}
}
