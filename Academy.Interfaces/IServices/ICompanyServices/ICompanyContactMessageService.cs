using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface ICompanyContactMessageService
	{
		Task SubmitMessageAsync(CreateCompanyContactMessageDTO dto);
		Task<IEnumerable<CompanyContactMessageDTO>> GetAllAsync(); // مضافة
		Task<CompanyContactMessageDTO?> GetByIdAsync(int id);     // مضافة
		Task<bool> DeleteAsync(int id);                    // مضافة
	}
}
