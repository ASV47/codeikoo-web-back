using AbstractionLayer;
using Academy.Application;
using Academy.Interfaces.IServices;
using Academy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helpers;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiExplorerSettings(GroupName = "Company")]
	public class FlexibilityItemsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<FlexibilityItemDto>>> GetAll()
		=> Ok(await _serviceManager.flexibilityItemService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<FlexibilityItemDto>> GetById(int id)
		{
			var result = await _serviceManager.flexibilityItemService.GetByIdAsync(id);

			if (result == null)
				return NotFound(new { Message = $"Flexibility Item with ID {id} not found." });

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromForm] CreateFlexibilityItemDto dto)
		{
			var path = DocumentSettings.UploadFile(dto.Icon, "Flexibility");
			await _serviceManager.flexibilityItemService.AddAsync(dto.Title, path);
			return Ok(new { Message = "Created Successfully" });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromForm] CreateFlexibilityItemDto dto)
		{
			var itemDto = await _serviceManager.flexibilityItemService.GetByIdAsync(id);
			if (itemDto == null) return NotFound();

			string? newPath = null;
			if (dto.Icon != null)
			{
				// مسح الصورة القديمة
				var oldRelativePath = itemDto.IconUrl.Replace("https://localhost:7048/", "");
				DocumentSettings.DeleteFile(oldRelativePath);

				// رفع الصورة الجديدة
				newPath = DocumentSettings.UploadFile(dto.Icon, "Flexibility");
			}

			await _serviceManager.flexibilityItemService.UpdateAsync(id, dto.Title, newPath);
			return Ok(new { Message = "Updated Successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var itemDto = await _serviceManager.flexibilityItemService.GetByIdAsync(id);
			if (itemDto == null) return NotFound();

			// مسح الصورة من الهارد
			var relativePath = itemDto.IconUrl.Replace("https://localhost:7048/", "");
			DocumentSettings.DeleteFile(relativePath);

			// مسح السجل من الداتابيز
			await _serviceManager.flexibilityItemService.DeleteAsync(id);
			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
