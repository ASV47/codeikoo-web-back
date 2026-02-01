using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.IServices;
using Academy.Web.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Academy.Web.Controllers.AcademyControllers
{
    [ApiExplorerSettings(GroupName = "Academy")]
    public class ImageSlidersController(IServiceManager serviceManager, IHubContext<MessageHub> hubContext) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ImageSliderDto>>> GetAll()
        => Ok(await serviceManager.ImageSliderService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImageSliderDto>> GetById(int id)
            => Ok(await serviceManager.ImageSliderService.GetByIdAsync(id));

        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<ActionResult<ImageSliderDto>> Add([FromForm] CreateImageSliderDto dto)
        //=> Ok(await serviceManager.ImageSliderService.AddAsync(dto));

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ImageSliderDto>> Add([FromForm] CreateImageSliderDto dto)
        {
            var result = await serviceManager.ImageSliderService.AddAsync(dto);

            // ✅ إضافة آمنة: لو الإيميل اتبعت، ابعت Welcome Message للـ Group بتاع الإيميل
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailKey = dto.Email.Trim().ToLowerInvariant();

                await hubContext.Clients.Group(emailKey).SendAsync(
                    "WelcomeMessage",
                    "أهلاً بيك 👋 تم تسجيل بريدك بنجاح في أكاديمية كوديكو . هنبلغك بكل جديد قريبًا!"
                );
            }

            return Ok(result);
        }


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
