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
	public class ExperiencesController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ExperienceDto>>> Get() // تم تعديل النوع هنا ليكون قائمة
			=> Ok(await _serviceManager.ExperienceService.GetAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<ExperienceDto>> GetById(int id)
		{
			var result = await _serviceManager.ExperienceService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<ExperienceDto>> Create(CreateExperienceDto dto) // استخدام ExperienceDto
			=> Ok(await _serviceManager.ExperienceService.CreateAsync(dto));

		[HttpPut("{id}")]
		public async Task<ActionResult<ExperienceDto>> Update(int id, CreateExperienceDto dto) // استخدام ExperienceDto
			=> Ok(await _serviceManager.ExperienceService.UpdateAsync(id, dto));

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _serviceManager.ExperienceService.DeleteAsync(id);
			return NoContent();
		}
	}
}
