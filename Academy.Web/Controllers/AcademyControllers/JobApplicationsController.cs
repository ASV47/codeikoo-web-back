using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Academy.Interfaces.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class JobApplicationsController(IServiceManager _serviceManager) : APIBaseController
	{

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<JobApplicationDto>> Add([FromForm] CreateJobApplicationDto dto)
        {
            var result = await _serviceManager.JobApplicationService.AddAsync(dto);
            return Ok(result);
        }



        [HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var result = await _serviceManager.JobApplicationService.DeleteAsync(id);
			return Ok(result);
		}


        [HttpGet]
        public async Task<ActionResult<PagedResult<JobApplicationDto>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var jobApps = await _serviceManager.JobApplicationService.GetAllAsync(pagination);
            return Ok(jobApps);
        }


        [HttpGet("{id}")]
		public async Task<ActionResult<JobApplicationDto?>> GetById(int id)
			=> Ok(await _serviceManager.JobApplicationService.GetByIdAsync(id));


		[HttpPut("{id}/restore")]
		public async Task<ActionResult<bool>> Restore(int id)
		{
			var result = await _serviceManager.JobApplicationService.RestoreAsync(id);
			return Ok(result);
		}

	}
}
