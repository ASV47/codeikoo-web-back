using Academy.Application.Repositories;
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
    public class ContactMessageService(IUnitOfWork _unitOfWork, IMapper _mapper) : IContactMessageService
    {
        public async Task<ContactMessageDto> AddAsync(CreateContactMessageDto dto)
        {
            var entity = _mapper.Map<ContactMessage>(dto);
            await _unitOfWork.GetRepository<ContactMessage, int>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactMessageDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<ContactMessage, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return false;
            repo.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ContactMessageDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.GetRepository<ContactMessage, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ContactMessageDto>>(entities);
        }

        public async Task<ContactMessageDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<ContactMessage, int>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ContactMessageDto>(entity);
        }
    }
}
