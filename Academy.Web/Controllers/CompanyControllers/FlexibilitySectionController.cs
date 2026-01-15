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
	public class FlexibilitySectionController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<FlexibilitySectionDto>> Get()
		{
			var result = await _serviceManager.flexibilitySectionService.GetAsync();
			if (result == null) return NotFound(new { Message = "Section data not found" });
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<FlexibilitySectionDto>> GetById(int id)
		{
			var result = await _serviceManager.flexibilitySectionService.GetByIdAsync(id);

			if (result == null)
				return NotFound(new { Message = $"Flexibility Section with ID {id} not found." });

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<FlexibilitySectionDto>> Create(CreateFlexibilitySectionDto dto)
		{
			var result = await _serviceManager.flexibilitySectionService.CreateAsync(dto);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateFlexibilitySectionDto dto)
		{
			var isUpdated = await _serviceManager.flexibilitySectionService.UpdateAsync(id, dto);

			if (!isUpdated)
				return NotFound(new { Message = "Section not found" });

			return Ok(new { Message = "Updated Successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.flexibilitySectionService.DeleteAsync(id);
			if (!isDeleted) return NotFound();
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
