using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers.AcademyControllers
{
    [ApiExplorerSettings(GroupName = "Academy")]
    public class ImageSlidersController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ImageSliderDto>>> GetAll()
        => Ok(await serviceManager.ImageSliderService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImageSliderDto>> GetById(int id)
            => Ok(await serviceManager.ImageSliderService.GetByIdAsync(id));

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ImageSliderDto>> Add([FromForm] CreateImageSliderDto dto)
        => Ok(await serviceManager.ImageSliderService.AddAsync(dto));


        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ImageSliderDto>> Update(int id, [FromForm] CreateImageSliderDto dto)
            => Ok(await serviceManager.ImageSliderService.UpdateAsync(id, dto));



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await serviceManager.ImageSliderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            await serviceManager.ImageSliderService.RestoreAsync(id);
            return NoContent();
        }
    }
}
