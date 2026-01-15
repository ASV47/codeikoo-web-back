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
	public class TechnologyHandler(IUnitOfWork unitOfWork, IMapper mapper) : ITechnologyService
	{
		public async Task<IEnumerable<TechnologyDto>> GetAllAsync()
		{
			var techs = await unitOfWork.GetRepository<Technology, int>().GetAllAsync();
			return mapper.Map<IEnumerable<TechnologyDto>>(techs);
		}
		public async Task<bool> UpdateAsync(int id, string imageUrl)
		{
			var repo = unitOfWork.GetRepository<Technology, int>();
			var tech = await repo.GetByIdAsync(id);
			if (tech == null) return false;

			if (!string.IsNullOrEmpty(imageUrl))
			{
				tech.TechnologyUrl = imageUrl;
			}

			repo.Update(tech);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
		public async Task AddAsync(string imageUrl)
		{
			await unitOfWork.GetRepository<Technology, int>().AddAsync(new Technology { TechnologyUrl = imageUrl });
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Technology, int>();
			var tech = await repo.GetByIdAsync(id);

			if (tech == null) return false;

			repo.Delete(tech);
			var result = await unitOfWork.SaveChangesAsync();

			return result > 0;
		}

		public async Task<TechnologyDto?> GetByIdAsync(int id)
		{
			var tech = await unitOfWork.GetRepository<Technology, int>().GetByIdAsync(id);
			return mapper.Map<TechnologyDto>(tech);
		}

	}
}
