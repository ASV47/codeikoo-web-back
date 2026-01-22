using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class InstructorApplicationsController(IServiceManager _serviceManager) : APIBaseController
	{
        //[HttpPost]
        //public async Task<ActionResult<InstructorApplicationDto>> Add([FromForm] CreateInstructorApplicationDto dto)
        //{
        //	var result = await _serviceManager.InstructorApplicationService.AddAsync(dto);
        //	return Ok(result);
        //}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<InstructorApplicationDto>> Add([FromForm] CreateInstructorApplicationDto dto)
        {
            var result = await _serviceManager.InstructorApplicationService.AddAsync(dto);
            return Ok(result);
        }



        [HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var result = await _serviceManager.InstructorApplicationService.DeleteAsync(id);
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<InstructorApplicationDto>>> GetAll()
		=> Ok(await _serviceManager.InstructorApplicationService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<InstructorApplicationDto?>> GetById(int id)
	   => Ok(await _serviceManager.InstructorApplicationService.GetByIdAsync(id));

		[HttpPut("{id}/restore")]
		public async Task<ActionResult<bool>> Restore(int id)
		{
			var result = await _serviceManager.InstructorApplicationService.RestoreAsync(id);
			return Ok(result);
		}

	}
}
