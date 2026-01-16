using Academy.Application.Services.AcademyServices;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class UnitLessonsController(IServiceManager serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<List<UnitLessonDto>>> GetAll([FromQuery] int? courseUnitId, [FromQuery] string? lang = "en")
		=> Ok(await serviceManager.UnitLessonService.GetAllAsync(courseUnitId, lang));

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UnitLessonDto>> GetById(int id, [FromQuery] string? lang = "en")
			=> Ok(await serviceManager.UnitLessonService.GetByIdAsync(id, lang));

		[HttpPost]
		public async Task<ActionResult<UnitLessonDto>> Add([FromBody] CreateUnitLessonDto dto)
			=> Ok(await serviceManager.UnitLessonService.AddAsync(dto));

		[HttpPut("{id:int}")]
		public async Task<ActionResult<UnitLessonDto>> Update(int id, [FromBody] CreateUnitLessonDto dto)
			=> Ok(await serviceManager.UnitLessonService.UpdateAsync(id, dto));

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await serviceManager.UnitLessonService.DeleteAsync(id);
			return NoContent();
		}



		[HttpPut("{id}/restore")]
		public async Task<IActionResult> Restore(int id)
		{
			await serviceManager.UnitLessonService.RestoreAsync(id);
			return NoContent();
		}

	}
}
