using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface ISiteContactService
	{
		Task<IEnumerable<SiteContactDto>> GetAllContactsAsync();
		Task<SiteContactDto> CreateContactAsync(CreateSiteContactDto dto);
		Task<SiteContactDto?> GetByIdAsync(int id);
		Task<SiteContactDto> UpdateContactAsync(int id, CreateSiteContactDto dto);
		Task DeleteContactAsync(int id);
	}
}
