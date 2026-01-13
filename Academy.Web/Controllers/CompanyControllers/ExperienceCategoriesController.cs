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
	public class ExperienceCategoriesController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ExperienceCategoryDto>>> GetAll()
		=> Ok(await _serviceManager.ExperienceCategoryService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<ExperienceCategoryDto>> GetById(int id)
		{
			var result = await _serviceManager.ExperienceCategoryService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<ExperienceCategoryDto>> Create(CreateExperienceCategoryDto dto)
			=> Ok(await _serviceManager.ExperienceCategoryService.CreateAsync(dto));

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateExperienceCategoryDto dto)
		{
			var result = await _serviceManager.ExperienceCategoryService.UpdateAsync(id, dto);
			return result ? Ok(new { Message = "Updated Successfully" }) : NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _serviceManager.ExperienceCategoryService.DeleteAsync(id);
			return NoContent();
		}
	}
}
