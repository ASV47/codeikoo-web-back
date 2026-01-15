using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IMissionService
	{
		Task<MissionDto> GetMissionAsync();
		Task<MissionDto?> GetByIdAsync(int id);
		Task<MissionDto> CreateMissionAsync(CreateMissionDto dto);
		Task<MissionDto> UpdateMissionAsync(CreateMissionDto dto);
		Task<bool> DeleteMissionAsync(int id); // مضافة
	}
}
