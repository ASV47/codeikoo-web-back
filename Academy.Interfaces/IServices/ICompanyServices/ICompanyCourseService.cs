using Academy.Interfaces.Pagination;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface ICompanyCourseService
	{
        Task<PagedResult<CompanyCourseDTO>> GetAllCoursesAsync(PaginationParams pagination);
        Task<CompanyCourseDTO> GetCourseById(int id);
		Task<CompanyCourseDTO> CreateCourseAsync(CreateCompanyCourseDTO dto);
		Task<CompanyCourseDTO> UpdateCourseAsync(int id, CreateCompanyCourseDTO dto);
		Task DeleteCourseAsync(int id);
		Task EnrollInCourseAsync(int courseId, CourseEnrollmentDTO dto);
	}
}
