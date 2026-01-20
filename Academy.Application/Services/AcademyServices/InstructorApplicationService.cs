using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.IServices.IAcademyServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
	public class InstructorApplicationService(IUnitOfWork _unitOfWork,
		IMapper _mapper, IFileStorageService fileStorage) : IInstructorApplicationService
	{
        public async Task<InstructorApplicationDto> AddAsync(CreateInstructorApplicationDto dto)
        {
            string cvUrl = string.Empty;

            if (dto.CvFile is not null)
                cvUrl = await fileStorage.UploadAsync(dto.CvFile, "InstructorCVs");

            var entity = _mapper.Map<InstructorApplication>(dto);
            entity.CvFilePath = cvUrl;

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await _unitOfWork.GetRepository<InstructorApplication, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InstructorApplicationDto>(entity);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<InstructorApplication, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null) return false;
            if (entity.IsDeleted) return true;

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<InstructorApplicationDto>> GetAllAsync()
		{
			var entities = await _unitOfWork.GetRepository<InstructorApplication, int>()
				.Query()
				.AsNoTracking()
				.Where(x => !x.IsDeleted)       // ✅ SoftDelete filter
				.OrderByDescending(x => x.Id)
				.ToListAsync();

			return _mapper.Map<IEnumerable<InstructorApplicationDto>>(entities);
		}

		public async Task<InstructorApplicationDto?> GetByIdAsync(int id)
		{
			var entity = await _unitOfWork.GetRepository<InstructorApplication, int>()
				.Query()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted); // ✅ SoftDelete filter

			return entity is null ? null : _mapper.Map<InstructorApplicationDto>(entity);
		}

		// ✅ Restore (زي ما اتفقنا)
		public async Task<bool> RestoreAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<InstructorApplication, int>();
			var entity = await repo.GetByIdAsync(id);

			if (entity is null) return false;
			if (!entity.IsDeleted) return true;

			entity.IsDeleted = false;
			AuditHelper.SetModified(entity, AuditDefaults.AdminId);

			repo.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}

}
