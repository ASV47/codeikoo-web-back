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
	public class ExperienceItemsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ExperienceItemDto>>> GetAll()
			=> Ok(await _serviceManager.ExperienceItemService.GetAllAsync());

		[HttpGet("ByCategory/{categoryId}")]
		public async Task<ActionResult<IEnumerable<ExperienceItemDto>>> GetByCategory(int categoryId)
			=> Ok(await _serviceManager.ExperienceItemService.GetByCategoryAsync(categoryId));

		[HttpGet("{id}")]
		public async Task<ActionResult<ExperienceItemDto>> GetById(int id)
		{
			var result = await _serviceManager.ExperienceItemService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<ExperienceItemDto>> Create(CreateExperienceItemDto dto)
			=> Ok(await _serviceManager.ExperienceItemService.CreateAsync(dto));

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateExperienceItemDto dto)
		{
			var result = await _serviceManager.ExperienceItemService.UpdateAsync(id, dto);
			return result ? Ok(new { Message = "Updated Successfully" }) : NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _serviceManager.ExperienceItemService.DeleteAsync(id);
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
