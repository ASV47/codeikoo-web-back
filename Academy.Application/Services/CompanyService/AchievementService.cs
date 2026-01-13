using AbstractionLayer;
using Academy.Interfaces.Interfaces;
using AutoMapper;
using CoreLayer.Entities;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class AchievementService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAchievementService
	{
		public async Task<IEnumerable<AchievementDto>> GetAllAsync()
		{
			var repo = _unitOfWork.GetRepository<Achievement, int>();
			var achievements = await repo.GetAllAsync();
			return _mapper.Map<IEnumerable<AchievementDto>>(achievements);
		}

		// 1. GetByIdAsync
		public async Task<AchievementDto?> GetByIdAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Achievement, int>();
			var achievement = await repo.GetByIdAsync(id);
			return _mapper.Map<AchievementDto>(achievement);
		}

		// 2. AddAsync
		public async Task<AchievementDto> AddAsync(CreateAchievementDTO dto)
		{
			var repo = _unitOfWork.GetRepository<Achievement, int>();
			var achievement = _mapper.Map<Achievement>(dto);

			await repo.AddAsync(achievement);
			await _unitOfWork.SaveChangesAsync(); //

			return _mapper.Map<AchievementDto>(achievement);
		}

		public async Task<bool> UpdateAsync(int id, CreateAchievementDTO dto)
		{
			var repo = _unitOfWork.GetRepository<Achievement, int>();
			var achievement = await repo.GetByIdAsync(id);

			if (achievement == null) return false;

			// تحديث البيانات يدوياً أو عبر Mapper
			achievement.Title = dto.Title;
			achievement.Number = dto.Number;

			repo.Update(achievement);
			return await _unitOfWork.SaveChangesAsync() > 0; //
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Achievement, int>();
			var achievement = await repo.GetByIdAsync(id);

			if (achievement == null) return false;

			repo.Delete(achievement);
			return await _unitOfWork.SaveChangesAsync() > 0; //
		}
	}
}
