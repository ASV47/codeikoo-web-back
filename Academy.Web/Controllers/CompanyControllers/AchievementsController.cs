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
	public class AchievementsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AchievementDto>>> GetAll()
		{
			var result = await _serviceManager.achievementService.GetAllAsync();
			return Ok(result);
		}

		// مضافة: GetById
		[HttpGet("{id}")]
		public async Task<ActionResult<AchievementDto>> GetById(int id)
		{
			var result = await _serviceManager.achievementService.GetByIdAsync(id);
			if (result == null) return NotFound();
			return Ok(result);
		}

		// مضافة: Create
		[HttpPost]
		public async Task<ActionResult<AchievementDto>> Create(CreateAchievementDTO dto)
		{
			var result = await _serviceManager.achievementService.AddAsync(dto);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateAchievementDTO dto)
		{
			var isUpdated = await _serviceManager.achievementService.UpdateAsync(id, dto);
			if (!isUpdated) return NotFound(new { Message = "Achievement not found" });
			return Ok(new { Message = "Updated Successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.achievementService.DeleteAsync(id);
			if (!isDeleted) return NotFound(new { Message = "Achievement not found" });
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
