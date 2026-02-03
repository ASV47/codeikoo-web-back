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

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<CourseUnitDto>> Update(int id, [FromBody] CreateCourseUnitDto dto)
		=> Ok(await serviceManager.CourseUnitService.UpdateAsync(id, dto));


        [HttpPost]
		public async Task<ActionResult<CourseUnitDto>> Add([FromBody] CreateCourseUnitDto dto)
			=> Ok(await serviceManager.CourseUnitService.AddAsync(dto));

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
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
