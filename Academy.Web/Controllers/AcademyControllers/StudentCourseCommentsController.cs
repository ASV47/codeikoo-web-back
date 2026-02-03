using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.IServices;
using Academy.Interfaces.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers.AcademyControllers
{
    [ApiExplorerSettings(GroupName = "Academy")]
    public class StudentCourseCommentsController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await serviceManager.StudentCourseCommentService.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<StudentCourseCommentDto>> Update(int id, [FromBody] StudentCourseCommentCreateDto dto)
        => Ok(await serviceManager.StudentCourseCommentService.UpdateAsync(id, dto));


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
