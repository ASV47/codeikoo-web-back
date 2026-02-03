using Academy.Application.Repositories;
using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
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
    public class ContactMessageService(IUnitOfWork _unitOfWork, IMapper _mapper) : IContactMessageService
    {
		public async Task<ContactMessageDto> AddAsync(CreateContactMessageDto dto)
		{
			var repo = _unitOfWork.GetRepository<ContactMessage, int>();

			var entity = _mapper.Map<ContactMessage>(dto);

			// CreatedBy/CreatedOn
			AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

			await repo.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<ContactMessageDto>(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<ContactMessage, int>();

			var entity = await repo.GetByIdAsync(id);
			if (entity is null) return false;

			if (entity.IsDeleted) return true;

			// SoftDelete بدل Remove/Delete الحقيقي
			AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

			// مع FindAsync غالبًا tracked ومش لازم Update
			// لكن خليها موجودة “أمان” لو أي وقت بقيت AsNoTracking
			repo.Update(entity);

			await _unitOfWork.SaveChangesAsync();
			return true;
		}

        public async Task<PagedResult<ContactMessageDto>> GetAllAsync(PaginationParams pagination)
        {
            if (pagination.PageNumber < 1) pagination.PageNumber = 1;

            var repo = _unitOfWork.GetRepository<ContactMessage, int>();

            var query = repo.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id); // ثابت قبل Skip/Take

            var totalCount = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            var entities = await query
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = _mapper.Map<List<ContactMessageDto>>(entities);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PagedResult<ContactMessageDto>
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
        public async Task<ContactMessageDto?> GetByIdAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<ContactMessage, int>();

			// الأفضل كده عشان الـ SoftDeleted مايرجعش أصلاً
			var entity = await repo.Query()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

			return entity is null ? null : _mapper.Map<ContactMessageDto>(entity);
		}

		public async Task<bool> RestoreAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<ContactMessage, int>();
			var entity = await repo.GetByIdAsync(id);
			if (entity is null) return false;

			if (!entity.IsDeleted) return true; // already active

			entity.IsDeleted = false;

			// ✅ Audit (Modified)
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

	}
}
