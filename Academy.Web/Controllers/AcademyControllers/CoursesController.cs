using Academy.Application.Services;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class CoursesController(IServiceManager serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
	   => Ok(await serviceManager.CourseService.GetAllAsync());

		[HttpGet("{id:int}")]
		public async Task<ActionResult<CourseDto>> GetById(int id)
			=> Ok(await serviceManager.CourseService.GetByIdAsync(id));

		// ✅ Search
		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<CourseDto>>> Search([FromQuery] string q)
			=> Ok(await serviceManager.CourseService.SearchAsync(q));

		[Authorize]
		[HttpPost]
		public async Task<ActionResult<CourseDto>> Add([FromForm] CreateCourseDto dto)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
						 ?? throw new UnauthorizedAccessException();

			return Ok(await serviceManager.CourseService.AddAsync(userId, dto));
		}

		[Authorize]
		[HttpPut("{id:int}")]
		public async Task<ActionResult<CourseDto>> Update(int id, [FromForm] CreateCourseDto dto)
			=> Ok(await serviceManager.CourseService.UpdateAsync(id, dto));

		[Authorize]
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await serviceManager.CourseService.DeleteAsync(id);
			return NoContent();
		}
	}
}
