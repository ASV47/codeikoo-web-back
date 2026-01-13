using AbstractionLayer;
using Academy.Interfaces.IServices;
using Academy.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
	public class ServiceController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult> GetAll()
			=> Ok(await _serviceManager.serviceHandler.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var result = await _serviceManager.serviceHandler.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult> Add(CreateServiceDTO dto) // تم إزالة [FromForm] لأننا نرسل JSON الآن
		{
			// نرسل dto.Icon مباشرة لأنه أصبح string يحتوي على اسم الصورة
			await _serviceManager.serviceHandler.AddAsync(dto);
			return Ok(new { Message = "Service Added Successfully" });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateServiceDTO dto)
		{
			var existing = await _serviceManager.serviceHandler.GetByIdAsync(id);
			if (existing == null) return NotFound(new { Message = "Service not found" });

			// نمرر dto.Icon مباشرة للسيرفس
			// السيرفس ستتحقق: إذا كان فارغاً ستحتفظ بالقديم، وإذا به قيمة ستحدثها
			var result = await _serviceManager.serviceHandler.UpdateAsync(id, dto);

			return result ? Ok(new { Message = "Updated Successfully" }) : BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var isDeleted = await _serviceManager.serviceHandler.DeleteAsync(id);
			if (!isDeleted) return NotFound();

			return Ok(new { Message = "Service Deleted Successfully" });
		}
	}
}
