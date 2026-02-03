using AbstractionLayer;
using Academy.Interfaces.IServices;
using Academy.Interfaces.Pagination;
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
	[Route("api/company/JopApp")]
	[ApiController]
	public class JobApplicationsController(IServiceManager _serviceManager) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Submit(CreateCompanyJopApplicationDTO dto)
		{
			Console.WriteLine($"Received terms_accepted: {dto.termsAccepted}");

			await _serviceManager.CompanyJobApplicationService.SubmitApplicationAsync(dto);
			return Ok(new { Message = "Application submitted successfully" });
		}

        [HttpGet]
        public async Task<ActionResult<PagedResult<CompanyJopApplicationDTO>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _serviceManager.CompanyJobApplicationService.GetAllAsync(pagination);
            return Ok(result);
        }


        [HttpGet("{id}")]
		public async Task<ActionResult<CompanyJopApplicationDTO>> GetById(int id)
		{
			var result = await _serviceManager.CompanyJobApplicationService.GetByIdAsync(id);
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.CompanyJobApplicationService.DeleteAsync(id);
			if (!isDeleted) return NotFound(new { Message = "Application not found" });
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
