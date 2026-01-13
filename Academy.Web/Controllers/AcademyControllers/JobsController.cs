using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<JobDto>>> GetAll()
		=> Ok(await _serviceManager.JobService.GetAllAsync());


		[HttpGet("{id}")]
		public async Task<ActionResult<JobDto?>> GetById(int id)
		=> Ok(await _serviceManager.JobService.GetByIdAsync(id));
	}
}
