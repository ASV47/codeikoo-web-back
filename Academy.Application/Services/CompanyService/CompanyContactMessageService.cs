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
	public class CompanyContactMessageService(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyContactMessageService
	{
		public async Task SubmitMessageAsync(CreateCompanyContactMessageDTO dto)
		{
			if (!dto.termsAccepted)
				throw new ArgumentException("You must accept the terms.");

			var message = mapper.Map<CompanyContactMessage>(dto);
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			await repo.AddAsync(message);
			await unitOfWork.SaveChangesAsync();
		}

		// 1. جلب كل الرسائل (للوحة التحكم)
		public async Task<IEnumerable<CompanyContactMessageDTO>> GetAllAsync()
		{
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			var messages = await repo.GetAllAsync();
			return mapper.Map<IEnumerable<CompanyContactMessageDTO>>(messages);
		}

		// 2. جلب رسالة محددة بالتفصيل
		public async Task<CompanyContactMessageDTO?> GetByIdAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			var message = await repo.GetByIdAsync(id);
			return mapper.Map<CompanyContactMessageDTO>(message);
		}

		// 3. حذف رسالة
		public async Task<bool> DeleteAsync(int id)
		{
			var repo = unitOfWork.GetRepository<CompanyContactMessage, int>();
			var message = await repo.GetByIdAsync(id);
			if (message == null) return false;

			repo.Delete(message);
			return await unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
