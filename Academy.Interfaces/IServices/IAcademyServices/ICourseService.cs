using Academy.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface ICourseService
	{
		Task<CourseDto> AddAsync(string userId, CreateCourseDto dto, string? lang = "en");

		Task<IEnumerable<CourseDto>> GetAllAsync(string? lang = "en");
		Task<CourseDto> GetByIdAsync(int id, string? lang = "en");

		Task<IEnumerable<CourseDto>> SearchAsync(string search, string? lang = "en");

		Task<CourseDto> UpdateAsync(int id, string userId, CreateCourseDto dto, string? lang = "en");
		Task DeleteAsync(int id);
		Task RestoreAsync(int id);
	}
}
