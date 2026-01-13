using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class JobApplicationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IJobApplicationService
    {
        public async Task<JobApplicationDto> AddAsync(CreateJobApplicationDto dto)
        {
            var jobRepo = _unitOfWork.GetRepository<Job, int>();
            var job = await jobRepo.GetByIdAsync(dto.JobId);
            if (job is null) throw new ArgumentException("Job not found.");

            string cvPath = string.Empty;
            if (dto.CvFile is not null)
                cvPath = DocumentSettings.UploadFile(dto.CvFile, "JobApplicationCVs");

            var entity = _mapper.Map<JobApplication>(dto);

            entity.CvFilePath = cvPath;

            await _unitOfWork.GetRepository<JobApplication, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<JobApplicationDto>(entity);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<JobApplication, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return false;

            if (!string.IsNullOrEmpty(entity.CvFilePath))
                DocumentSettings.DeleteFile(entity.CvFilePath);

            repo.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.GetRepository<JobApplication, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<JobApplicationDto>>(entities);
        }

        public async Task<JobApplicationDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<JobApplication, int>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<JobApplicationDto>(entity);
        }
    }
}
