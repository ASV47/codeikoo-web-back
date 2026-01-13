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
    public class JobService(IUnitOfWork _unitOfWork, IMapper _mapper) : IJobService
    {
        public async Task<JobDto> AddAsync(CreateJobDto dto)
        {
            var entity = _mapper.Map<Job>(dto);
            await _unitOfWork.GetRepository<Job, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<JobDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Job, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return false;
            repo.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.GetRepository<Job, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<JobDto>>(entities);
        }

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<Job, int>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<JobDto>(entity);
        }
    }
}
