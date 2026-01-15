using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IAboutUsService
	{
		Task<IEnumerable<AboutUsDto>> GetAllAsync();
		Task<AboutUsDto?> GetByIdAsync(int id); // مضافة
		Task<AboutUsDto> AddAsync(CreateAboutUsDto dto); // مضافة
		Task<bool> UpdateAsync(int id, CreateAboutUsDto dto);
		Task<bool> DeleteAsync(int id); // مضافة
	}
}
