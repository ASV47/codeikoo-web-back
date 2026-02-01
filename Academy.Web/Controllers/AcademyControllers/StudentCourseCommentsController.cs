using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers.AcademyControllers
{
    public class StudentCourseCommentsController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await serviceManager.StudentCourseCommentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await serviceManager.StudentCourseCommentService.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCourseCommentCreateDto dto)
        {
            var created = await serviceManager.StudentCourseCommentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await serviceManager.StudentCourseCommentService.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
