using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IWebSettingsService
    {
		Task<IEnumerable<WebSettingsDto>> GetAllAsync();
		Task<WebSettingsDto?> GetByIdAsync(int id);
		Task<WebSettingsDto> CreateAsync(CreateWebSettingsDto dto);
		Task<WebSettingsDto> UpdateAsync(int id, CreateWebSettingsDto dto);
		Task<bool> DeleteAsync(int id);
	}
}
