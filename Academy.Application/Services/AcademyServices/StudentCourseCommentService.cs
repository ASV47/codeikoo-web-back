using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices.IAcademyServices;
using Academy.Interfaces.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PagedResult<StudentCourseCommentDto>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            // لازم يكون عندك Queryable هنا
            var query = _repo.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)          // لو الكيان فيه SoftDelete
                .OrderByDescending(x => x.Id);     // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = mapper.Map<List<StudentCourseCommentDto>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<StudentCourseCommentDto>
            {
                Items = items,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = pagination.PageNumber > 1,
                HasNextPage = pagination.PageNumber < totalPages
            };
        }

        public async Task<StudentCourseCommentDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<StudentCourseCommentDto>(entity);
        }

        public async Task<StudentCourseCommentDto> UpdateAsync(int id, StudentCourseCommentCreateDto dto)
        {
            var repo = unitOfWork.GetRepository<StudentCourseComment, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Comment not found.");

            mapper.Map(dto, entity);

            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<StudentCourseCommentDto>(entity);
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
