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
	[Route("api/company/CompanyCourse")]
	[ApiController]
	public class CompanyCourseController(IServiceManager _serviceManager) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CompanyCourseDTO>>> GetAllCourses()
		=> Ok(await _serviceManager.CompanyCourseService.GetAllCoursesAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyCourseDTO>> GetById(int id)
		{
			var course = await _serviceManager.CompanyCourseService.GetCourseById(id);
			return course == null ? NotFound() : Ok(course);
		}

		[HttpPost]
		public async Task<ActionResult<CompanyCourseDTO>> Create(CreateCompanyCourseDTO dto)
			=> Ok(await _serviceManager.CompanyCourseService.CreateCourseAsync(dto));

		[HttpPut("{id}")]
		public async Task<ActionResult<CompanyCourseDTO>> Update(int id, CreateCompanyCourseDTO dto)
			=> Ok(await _serviceManager.CompanyCourseService.UpdateCourseAsync(id, dto));

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _serviceManager.CompanyCourseService.DeleteCourseAsync(id);
			return NoContent();
		}

		[HttpPost("{id}/enroll")]
		public async Task<IActionResult> Enroll(int id, CourseEnrollmentDTO dto)
		{
			await _serviceManager.CompanyCourseService.EnrollInCourseAsync(id, dto);
			return Ok("Enrolled successfully");
		}
	}
}
