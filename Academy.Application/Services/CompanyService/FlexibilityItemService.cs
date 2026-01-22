using AbstractionLayer;
using AutoMapper;
using CoreLayer.Entities;
using Academy.Interfaces.Interfaces;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Academy.Interfaces.IServices.IAcademyServices;

namespace ServiceLayer
{
	public class FlexibilityItemService(IUnitOfWork _unitOfWork,
        IMapper _mapper, IFileStorageService fileStorage) : IFlexibilityItemService
	{
		public async Task<IEnumerable<FlexibilityItemDto>> GetAllAsync()
		{
			var repo = _unitOfWork.GetRepository<FlexibilityItem, int>();
			var items = await repo.GetAllAsync();
			return _mapper.Map<IEnumerable<FlexibilityItemDto>>(items);
		}


        public async Task AddAsync(string title, IFormFile? icon)
        {
            string iconUrl = string.Empty;

            if (icon is not null)
                iconUrl = await fileStorage.UploadAsync(icon, "Flexibility");

            var item = new FlexibilityItem { Title = title, IconUrl = iconUrl };

            await _unitOfWork.GetRepository<FlexibilityItem, int>().AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<FlexibilityItemDto?> GetByIdAsync(int id)
		{
			var item = await _unitOfWork.GetRepository<FlexibilityItem, int>().GetByIdAsync(id);
			return _mapper.Map<FlexibilityItemDto>(item);
		}

        public async Task<bool> UpdateAsync(int id, string title, IFormFile? icon)
        {
            var repo = _unitOfWork.GetRepository<FlexibilityItem, int>();
            var item = await repo.GetByIdAsync(id);
            if (item == null) return false;

            item.Title = title;

            if (icon is not null)
            {
                // امسح القديم من Uploadcare
                if (!string.IsNullOrWhiteSpace(item.IconUrl))
                    await fileStorage.DeleteAsync(item.IconUrl);

                // ارفع الجديد
                item.IconUrl = await fileStorage.UploadAsync(icon, "Flexibility");
            }

            repo.Update(item);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<FlexibilityItem, int>();
            var item = await repo.GetByIdAsync(id);
            if (item == null) return false;

            // احذف الأيقونة من Uploadcare
            if (!string.IsNullOrWhiteSpace(item.IconUrl))
                await fileStorage.DeleteAsync(item.IconUrl);

            repo.Delete(item);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
