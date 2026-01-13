using AbstractionLayer;
using Academy.Interfaces.IServices;
using Academy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiExplorerSettings(GroupName = "Company")]
	[Route("api/company/ContactMessage")]
	[ApiController]
	public class CompanyContactMessageController(IServiceManager _serviceManager) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult> Submit(CreateCompanyContactMessageDTO dto)
		{
			//await _serviceManager.ContactMessageService.SubmitMessageAsync(dto);
			//return Ok(new { Message = "Message sent successfully" });
			Console.WriteLine($"Received terms_accepted: {dto.termsAccepted}");

			await _serviceManager.CompanyContactMessageService.SubmitMessageAsync(dto);
			return Ok(new { Message = "Application submitted successfully" });
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CompanyContactMessageDTO>>> GetAll()
		{
			var result = await _serviceManager.ContactMessageService.GetAllAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyContactMessageDTO>> GetById(int id)
		{
			var result = await _serviceManager.ContactMessageService.GetByIdAsync(id);
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.ContactMessageService.DeleteAsync(id);
			if (!isDeleted) return NotFound(new { Message = "Message not found" });
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
