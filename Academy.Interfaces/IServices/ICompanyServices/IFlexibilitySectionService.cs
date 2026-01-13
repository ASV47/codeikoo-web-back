using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IFlexibilitySectionService
	{
		Task<FlexibilitySectionDto?> GetAsync();
		Task<CreateFlexibilitySectionDto> CreateAsync(CreateFlexibilitySectionDto dto); // مضافة
		Task<FlexibilitySectionDto?> GetByIdAsync(int id);
		Task<bool> UpdateAsync(int id, CreateFlexibilitySectionDto dto);
		Task<bool> DeleteAsync(int id); // مضافة
	}
}
