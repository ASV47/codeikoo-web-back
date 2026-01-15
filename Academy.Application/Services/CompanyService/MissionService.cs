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
	public class MissionService(IUnitOfWork unitOfWork, IMapper mapper) : IMissionService
	{
		public async Task<MissionDto> GetMissionAsync()
		{
			var repo = unitOfWork.GetRepository<Mission, int>();
			var mission = await repo.GetAllAsync();
			var first = mission.FirstOrDefault() ?? throw new InvalidOperationException("No mission found.");
			return mapper.Map<MissionDto>(first);
		}

		public async Task<MissionDto?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Mission, int>();
			var entity = await repo.GetByIdAsync(id);

			return mapper.Map<MissionDto>(entity);
		}
		public async Task<MissionDto> CreateMissionAsync(CreateMissionDto dto)
		{
			var Mission = mapper.Map<CreateMissionDto, Mission>(dto);
			var repo = unitOfWork.GetRepository<Mission, int>();
			await repo.AddAsync(Mission);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<Mission, MissionDto>(Mission);
		}

		public async Task<MissionDto> UpdateMissionAsync(CreateMissionDto dto)
		{
			var repo = unitOfWork.GetRepository<Mission, int>();
			var mission = await repo.GetAllAsync();
			var existing = mission.FirstOrDefault() ?? throw new InvalidOperationException("No mission found.");
			mapper.Map(dto, existing);
			repo.Update(existing);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<MissionDto>(existing);
		}

		public async Task<bool> DeleteMissionAsync(int id)
		{
			var repo = unitOfWork.GetRepository<Mission, int>();
			var mission = await repo.GetByIdAsync(id);
			if (mission == null) return false;

			repo.Delete(mission);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
