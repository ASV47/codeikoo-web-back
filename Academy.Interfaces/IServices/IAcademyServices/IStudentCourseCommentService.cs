using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices.IAcademyServices
{
    public interface IStudentCourseCommentService
    {
        Task<PagedResult<StudentCourseCommentDto>> GetAllAsync(PaginationParams pagination);
        Task<StudentCourseCommentDto?> GetByIdAsync(int id);
        Task<StudentCourseCommentDto> UpdateAsync(int id, StudentCourseCommentCreateDto dto);
        Task<StudentCourseCommentDto> CreateAsync(StudentCourseCommentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
