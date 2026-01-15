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
	public class AboutUsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AboutUsDto>>> GetAll()
			=> Ok(await _serviceManager.aboutUsService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<AboutUsDto>> GetById(int id)
		{
			var result = await _serviceManager.aboutUsService.GetByIdAsync(id);
			return result != null ? Ok(result) : NotFound();
		}

		[HttpPost]
		public async Task<ActionResult<AboutUsDto>> Create(CreateAboutUsDto dto)
		{
			var result = await _serviceManager.aboutUsService.AddAsync(dto);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, CreateAboutUsDto dto)
		{
			var result = await _serviceManager.aboutUsService.UpdateAsync(id, dto);
			return result ? Ok(new { Message = "Updated Successfully" }) : NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _serviceManager.aboutUsService.DeleteAsync(id);
			return result ? Ok(new { Message = "Deleted Successfully" }) : NotFound();
		}
	}
}
