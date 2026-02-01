using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices.IAcademyServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class StudentCourseCommentService(IUnitOfWork unitOfWork, IMapper mapper) : IStudentCourseCommentService
    {
        private readonly IGenericRepository<StudentCourseComment, int> _repo =
    unitOfWork.GetRepository<StudentCourseComment, int>();

        public async Task<IEnumerable<StudentCourseCommentDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();

            return mapper.Map<IEnumerable<StudentCourseCommentDto>>(entities);
        }

        public async Task<StudentCourseCommentDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<StudentCourseCommentDto>(entity);
        }

        public async Task<StudentCourseCommentDto> CreateAsync(StudentCourseCommentCreateDto dto)
        {
            var entity = mapper.Map<StudentCourseComment>(dto);

            await _repo.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<StudentCourseCommentDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;

            _repo.Delete(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
