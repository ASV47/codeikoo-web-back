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
		public async Task<ActionResult<List<UnitLessonDto>>> GetAll([FromQuery] int? courseUnitId)
	   => Ok(await serviceManager.UnitLessonService.GetAllAsync(courseUnitId));

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UnitLessonDto>> GetById(int id)
			=> Ok(await serviceManager.UnitLessonService.GetByIdAsync(id));

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
	}
}
