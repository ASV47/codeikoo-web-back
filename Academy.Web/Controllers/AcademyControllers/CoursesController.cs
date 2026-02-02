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

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        // => Ok(await serviceManager.CourseService.GetAllAsync());

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll([FromQuery] string? CourseName)
	    => Ok(await serviceManager.CourseService.GetAllAsync(CourseName));


        [Authorize]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<CourseDto>> GetById(int id)
			=> Ok(await serviceManager.CourseService.GetByIdAsync(id));

		//[Authorize]
		//[HttpGet("search")]
		//public async Task<ActionResult<IEnumerable<CourseDto>>> Search([FromQuery] string q)
		//	=> Ok(await serviceManager.CourseService.SearchAsync(q));
       
		
		[Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CourseDto>> Add([FromForm] CreateCourseDto dto)
        {
            // userId حسب نظامك: من التوكن مثلاً
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await serviceManager.CourseService.AddAsync(userId!, dto);
            return Ok(result);
        }


        [Authorize]
		[HttpPut("{id:int}")]
		[Consumes("multipart/form-data")]
		public async Task<ActionResult<CourseDto>> Update(int id, [FromForm] CreateCourseDto dto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrWhiteSpace(userId))
				return Unauthorized();

			return Ok(await serviceManager.CourseService.UpdateAsync(id, userId, dto));
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
