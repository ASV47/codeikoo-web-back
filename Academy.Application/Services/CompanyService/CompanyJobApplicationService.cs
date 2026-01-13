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
	public class CompanyJobApplicationService(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyJobApplicationService
	{
		public async Task SubmitApplicationAsync(CreateCompanyJopApplicationDTO dto)
		{
			try
			{
				if (!dto.termsAccepted)
					throw new ArgumentException("You must accept the terms.");

				var application = mapper.Map<CompanyJopApplication>(dto);
				var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
				await repo.AddAsync(application);
				await unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Log ex.Message
				throw; // أو return BadRequest(ex.Message)
			}
		}

		// 1. جلب كل طلبات التوظيف
		public async Task<IEnumerable<CompanyJopApplicationDTO>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
			var applications = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<CompanyJopApplicationDTO>>(applications);
		}

		// 2. جلب طلب توظيف معين بالتفصيل
		public async Task<CompanyJopApplicationDTO?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
			var application = await repo.GetByIdAsync(id);
			return mapper.Map<CompanyJopApplicationDTO>(application);
		}

		// 3. حذف طلب توظيف
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyJopApplication, int>();
			var application = await repo.GetByIdAsync(id);
			if (application == null) return false;

			repo.Delete(application);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
