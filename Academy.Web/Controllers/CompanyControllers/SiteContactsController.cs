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
	public class SiteContactsController(IServiceManager _serviceManager) : APIBaseController
	{
        [HttpGet]
        public async Task<ActionResult<PagedResult<SiteContactDto>>> GetAll([FromQuery] PaginationParams pagination)
		=> Ok(await _serviceManager.SiteContactService.GetAllContactsAsync(pagination));

        [HttpPost]
		public async Task<ActionResult<SiteContactDto>> Create(CreateSiteContactDto dto)
			=> Ok(await _serviceManager.SiteContactService.CreateContactAsync(dto));

		[HttpGet("{id}")]
		public async Task<ActionResult<SiteContactDto>> GetById(int id)
		{
			var result = await _serviceManager.SiteContactService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SiteContactDto>> Update(int id, CreateSiteContactDto dto)
			=> Ok(await _serviceManager.SiteContactService.UpdateContactAsync(id, dto));

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _serviceManager.SiteContactService.DeleteContactAsync(id);
			return NoContent();
		}
	}
}
