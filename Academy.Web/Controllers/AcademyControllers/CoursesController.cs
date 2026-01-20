using Academy.Application.Services;
using Academy.Application.Services.AcademyServices;
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

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll([FromQuery] string? lang = "en")
	  => Ok(await serviceManager.CourseService.GetAllAsync(lang));

		[Authorize]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<CourseDto>> GetById(int id, [FromQuery] string? lang = "en")
			=> Ok(await serviceManager.CourseService.GetByIdAsync(id, lang));

		[Authorize]
		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<CourseDto>>> Search([FromQuery] string q, [FromQuery] string? lang = "en")
			=> Ok(await serviceManager.CourseService.SearchAsync(q, lang));

		[Authorize]
		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<ActionResult<CourseDto>> Add([FromForm] CreateCourseDto dto/*, [FromQuery] string? lang = "en"*/)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrWhiteSpace(userId))
				return Unauthorized();

			return Ok(await serviceManager.CourseService.AddAsync(userId, dto));
		}

		[Authorize]
		[HttpPut("{id:int}")]
		[Consumes("multipart/form-data")]
		public async Task<ActionResult<CourseDto>> Update(int id, [FromForm] CreateCourseDto dto, [FromQuery] string? lang = "en")
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrWhiteSpace(userId))
				return Unauthorized();

			return Ok(await serviceManager.CourseService.UpdateAsync(id, userId, dto, lang));
		}


		[Authorize]
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await serviceManager.CourseService.DeleteAsync(id);
			return NoContent();
		}

		[Authorize]
		[HttpPut("{id}/restore")]
		public async Task<IActionResult> Restore(int id)
		{
			await serviceManager.CourseService.RestoreAsync(id);
			return NoContent();
		}

	}
}
