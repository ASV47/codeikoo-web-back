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
	public class WebSettingsController(IServiceManager _serviceManager) : APIBaseController
    {
		[HttpGet]
		public async Task<ActionResult<IEnumerable<WebSettingsDto>>> GetAll()
		=> Ok(await _serviceManager.webSettingsService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<WebSettingsDto>> GetById(int id)
		{
			var result = await _serviceManager.webSettingsService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<WebSettingsDto>> Create(CreateWebSettingsDto dto)
		{
			var result = await _serviceManager.webSettingsService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<WebSettingsDto>> Update(int id, CreateWebSettingsDto dto)
		{
			try
			{
				var result = await _serviceManager.webSettingsService.UpdateAsync(id, dto);
				return Ok(result);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.webSettingsService.DeleteAsync(id);
			if (!isDeleted) return NotFound();

			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
