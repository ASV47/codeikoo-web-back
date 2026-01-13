using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class CourseUnitService(IUnitOfWork unitOfWork, IMapper mapper) : ICourseUnitService
    {
        public async Task<CourseUnitDto> AddAsync(CreateCourseUnitDto dto)
        {
            var course = await unitOfWork.GetRepository<Course, int>().GetByIdAsync(dto.CourseId);
            if (course is null) throw new ArgumentException("Course not found.");

            var entity = mapper.Map<CourseUnit>(dto);

            await unitOfWork.GetRepository<CourseUnit, int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<CourseUnitDto>(entity);
        }

        public async Task<List<CourseUnitDto>> GetAllAsync(int? courseId = null)
        {
            var query = unitOfWork.GetRepository<CourseUnit, int>()
                .Query()
                .AsNoTracking();

            if (courseId.HasValue)
                query = query.Where(u => u.CourseId == courseId.Value);

            var entities = await query
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return mapper.Map<List<CourseUnitDto>>(entities);
        }

        public async Task<CourseUnitDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.GetRepository<CourseUnit, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) throw new ArgumentException("Course unit not found.");
            return mapper.Map<CourseUnitDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var repo = unitOfWork.GetRepository<CourseUnit, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) throw new ArgumentException("Course unit not found.");

            repo.Delete(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
