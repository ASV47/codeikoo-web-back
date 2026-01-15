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

namespace ServiceLayer
{
	public class FlexibilitySectionService(IUnitOfWork _unitOfWork, IMapper _mapper) : IFlexibilitySectionService
	{
		public async Task<FlexibilitySectionDto?> GetAsync()
		{
			var repo = _unitOfWork.GetRepository<FlexibilitySection, int>();
			// جلب أول سجل متاح للقسم
			var section = (await repo.GetAllAsync()).FirstOrDefault();
			return _mapper.Map<FlexibilitySectionDto>(section);
		}

		public async Task<FlexibilitySectionDto?> GetByIdAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<FlexibilitySection, int>();
			var entity = await repo.GetByIdAsync(id);

			// تحويل الكيان إلى DTO باستخدام الـ Mapper
			return _mapper.Map<FlexibilitySectionDto>(entity);
		}

		// 1. CreateAsync (لإضافة البيانات لأول مرة)
		public async Task<CreateFlexibilitySectionDto> CreateAsync(CreateFlexibilitySectionDto dto)
		{
			var repo = _unitOfWork.GetRepository<FlexibilitySection, int>();
			var entity = _mapper.Map<FlexibilitySection>(dto);

			await repo.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CreateFlexibilitySectionDto>(entity);
		}

		public async Task<bool> UpdateAsync(int id, CreateFlexibilitySectionDto dto)
		{
			var repo = _unitOfWork.GetRepository<FlexibilitySection, int>();
			var section = await repo.GetByIdAsync(id);

			if (section == null) return false;

			_mapper.Map(dto, section);

			repo.Update(section);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		// 2. DeleteAsync
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<FlexibilitySection, int>();
			var section = await repo.GetByIdAsync(id);

			if (section == null) return false;

			repo.Delete(section);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
