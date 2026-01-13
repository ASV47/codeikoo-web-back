using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class InstructorApplicationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IInstructorApplicationService
    {
        public async Task<InstructorApplicationDto> AddAsync(CreateInstructorApplicationDto dto)
        {
            string cvPath = string.Empty;

            if (dto.CvFile is not null)
                cvPath = DocumentSettings.UploadFile(dto.CvFile, "InstructorCVs");

            var entity = _mapper.Map<InstructorApplication>(dto);

            entity.CvFilePath = cvPath;

            await _unitOfWork.GetRepository<InstructorApplication, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InstructorApplicationDto>(entity);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<InstructorApplication, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return false;

            // حذف الملف لو موجود
            if (!string.IsNullOrEmpty(entity.CvFilePath))
                DocumentSettings.DeleteFile(entity.CvFilePath);

            repo.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InstructorApplicationDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.GetRepository<InstructorApplication, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<InstructorApplicationDto>>(entities);
        }

        public async Task<InstructorApplicationDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<InstructorApplication, int>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<InstructorApplicationDto>(entity);
        }
    }
}
