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
        Task<CourseDto> AddAsync(string userId, CreateCourseDto dto);
        Task<IEnumerable<CourseDto>> GetAllAsync(string? search = null);
        Task<CourseDto> GetByIdAsync(int id);
        //Task<IEnumerable<CourseDto>> SearchAsync(string search);
        Task<CourseDto> UpdateAsync(int id, string userId, CreateCourseDto dto);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
    }
}
