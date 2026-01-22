using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices.IAcademyServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class ImageSliderService(IUnitOfWork unitOfWork,IMapper mapper,
                        IFileStorageService fileStorage) : IImageSliderService
    {
        public async Task<ImageSliderDto> AddAsync(CreateImageSliderDto dto)
        {
            var imageUrl = string.Empty;
            var panarUrl = string.Empty;

            if (dto.Image is not null)
                imageUrl = await fileStorage.UploadAsync(dto.Image, "ImageSlider");

            if (dto.ImagePanar is not null)
                panarUrl = await fileStorage.UploadAsync(dto.ImagePanar, "ImageSlider");

            var entity = new ImageSlider
            {
                ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl,
                ImagePanar = string.IsNullOrWhiteSpace(panarUrl) ? null : panarUrl,
                Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim()
            };

            AuditHelper.SetCreated(entity, AuditDefaults.AdminId);

            await unitOfWork.GetRepository<ImageSlider, int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<ImageSliderDto>(entity);
        }


        public async Task<List<ImageSliderDto>> GetAllAsync()
        {
            var entities = await unitOfWork.GetRepository<ImageSlider, int>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return mapper.Map<List<ImageSliderDto>>(entities);
        }

        public async Task<ImageSliderDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.GetRepository<ImageSlider, int>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity is null)
                throw new ArgumentException("Image slider item not found.");

            return mapper.Map<ImageSliderDto>(entity);
        }

        public async Task<ImageSliderDto> UpdateAsync(int id, CreateImageSliderDto dto)
        {
            var repo = unitOfWork.GetRepository<ImageSlider, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Image slider item not found.");

            // Image
            if (dto.Image is not null)
            {
                if (!string.IsNullOrWhiteSpace(entity.ImageUrl))
                {
                    try { await fileStorage.DeleteAsync(entity.ImageUrl); }
                    catch { /* log لو عندك */ }
                }

                entity.ImageUrl = await fileStorage.UploadAsync(dto.Image, "ImageSlider");
            }

            // ImagePanar
            if (dto.ImagePanar is not null)
            {
                if (!string.IsNullOrWhiteSpace(entity.ImagePanar))
                {
                    try { await fileStorage.DeleteAsync(entity.ImagePanar); }
                    catch { /* log لو عندك */ }
                }

                entity.ImagePanar = await fileStorage.UploadAsync(dto.ImagePanar, "ImageSlider");
            }

            // Email (يتغير فقط لو اتبعت في الفورم)
            // - لو اتبعت فاضي => يمسحه (null)
            if (dto.Email is not null)
                entity.Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim();

            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<ImageSliderDto>(entity);
        }


        public async Task DeleteAsync(int id)
        {
            var repo = unitOfWork.GetRepository<ImageSlider, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null || entity.IsDeleted)
                throw new ArgumentException("Image slider item not found.");

            AuditHelper.SetSoftDeleted(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var repo = unitOfWork.GetRepository<ImageSlider, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                throw new ArgumentException("Image slider item not found.");

            if (!entity.IsDeleted) return;

            entity.IsDeleted = false;
            AuditHelper.SetModified(entity, AuditDefaults.AdminId);

            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
