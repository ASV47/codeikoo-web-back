using Academy.Interfaces.DTOs.AcademyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices.IAcademyServices
{
    public interface IStudentCourseCommentService
    {
        Task<IEnumerable<StudentCourseCommentDto>> GetAllAsync();
        Task<StudentCourseCommentDto?> GetByIdAsync(int id);
        Task<StudentCourseCommentDto> CreateAsync(StudentCourseCommentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
