using Academy.Interfaces.Pagination;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface ICompanyJobApplicationService
	{
		Task SubmitApplicationAsync(CreateCompanyJopApplicationDTO dto);
        Task<PagedResult<CompanyJopApplicationDTO>> GetAllAsync(PaginationParams pagination);
        // مضافة
        Task<CompanyJopApplicationDTO?> GetByIdAsync(int id);      // مضافة
		Task<bool> DeleteAsync(int id);                     // مضافة
	}
}
