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
	public class MissionController(IServiceManager _serviceManager) : APIBaseController
    {
		[HttpGet]
		public async Task<ActionResult<MissionDto>> Get()
	   => Ok(await _serviceManager.MissionService.GetMissionAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<MissionDto>> GetById(int id)
		{
			var result = await _serviceManager.MissionService.GetByIdAsync(id);

			if (result == null)
				return NotFound(new { Message = $"Mission with ID {id} not found." });
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<MissionDto>> Create(CreateMissionDto dto)
			=> Ok(await _serviceManager.MissionService.CreateMissionAsync(dto));

		[HttpPut]
		public async Task<ActionResult<MissionDto>> Update(CreateMissionDto dto)
			=> Ok(await _serviceManager.MissionService.UpdateMissionAsync(dto));


		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.MissionService.DeleteMissionAsync(id);
			if (!isDeleted) return NotFound();
			return Ok(new { Message = "Mission deleted successfully" });
		}
	}
}
