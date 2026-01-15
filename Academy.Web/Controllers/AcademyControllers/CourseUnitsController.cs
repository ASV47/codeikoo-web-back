using Academy.Application.Services.AcademyServices;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class CourseUnitsController(IServiceManager serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<List<CourseUnitDto>>> GetAll([FromQuery] int? courseId)
		=> Ok(await serviceManager.CourseUnitService.GetAllAsync(courseId));

		[HttpGet("{id:int}")]
		public async Task<ActionResult<CourseUnitDto>> GetById(int id)
			=> Ok(await serviceManager.CourseUnitService.GetByIdAsync(id));

		[HttpPost]
		public async Task<ActionResult<CourseUnitDto>> Add([FromBody] CreateCourseUnitDto dto)
			=> Ok(await serviceManager.CourseUnitService.AddAsync(dto));


		[HttpDelete("{id:int}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			await serviceManager.CourseUnitService.DeleteAsync(id);
			return NoContent();
		}

		[HttpPut("{id}/restore")]
		public async Task<IActionResult> Restore(int id)
		{
			await serviceManager.CourseUnitService.RestoreAsync(id);
			return NoContent();
		}

	}
}
