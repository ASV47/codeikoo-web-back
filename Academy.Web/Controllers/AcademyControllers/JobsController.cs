using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Academy.Interfaces.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class JobsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpPost]
		public async Task<ActionResult<JobDto>> Add([FromBody] CreateJobDto dto)
		=> Ok(await _serviceManager.JobService.AddAsync(dto));


		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var Job = await _serviceManager.JobService.DeleteAsync(id);
			return Ok(Job);
		}

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<JobDto>> Update(int id, [FromBody] CreateJobDto dto)
		=> Ok(await _serviceManager.JobService.UpdateAsync(id, dto));


        [HttpGet]
        public async Task<ActionResult<PagedResult<JobDto>>> GetAll([FromQuery] PaginationParams pagination)
	    => Ok(await _serviceManager.JobService.GetAllAsync(pagination));


        [HttpGet("{id}")]
		public async Task<ActionResult<JobDto?>> GetById(int id)
		=> Ok(await _serviceManager.JobService.GetByIdAsync(id));


		[HttpPut("{id}/restore")]
		public async Task<ActionResult<bool>> Restore(int id)
		{
			var result = await _serviceManager.JobService.RestoreAsync(id);
			return Ok(result);
		}

	}
}
