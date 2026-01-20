using AbstractionLayer;
using Academy.Application;
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
	public class TechnologiesController(IServiceManager _serviceManager, IWebHostEnvironment env) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult> GetAll() 
		=> Ok(await _serviceManager.TechnologyService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<TechnologyDto>> GetById(int id)
		{
			var result = await _serviceManager.TechnologyService.GetByIdAsync(id);

			if (result == null)
				return NotFound(new { Message = $"Technology with ID {id} not found." });

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> Add([FromForm] CreateTechnologyDto dto)
		{
			var path = DocumentSettings.UploadFile(dto.Image, "Logo", env);
			await _serviceManager.TechnologyService.AddAsync(path);
			return Ok(new { Message = "Added Successfully" });
		}


		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromForm] CreateTechnologyDto dto)
		{
			var existing = await _serviceManager.TechnologyService.GetByIdAsync(id);
			if (existing == null) return NotFound();

			string? finalPath = null;
			if (dto.Image != null)
			{
				var oldPath = existing.TechnologyUrl.Replace("https://localhost:7048", "");
				DocumentSettings.DeleteFile(oldPath);

				finalPath = DocumentSettings.UploadFile(dto.Image, "Technologies", env);
			}

			var result = await _serviceManager.TechnologyService.UpdateAsync(id, finalPath!);
			return result ? Ok(new { Message = "Updated Successfully" }) : BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var techDto = await _serviceManager.TechnologyService.GetByIdAsync(id);
			if (techDto == null) return NotFound();

			var relativePath = techDto.TechnologyUrl.Replace("https://localhost:7048/", "");

			DocumentSettings.DeleteFile(relativePath);
			await _serviceManager.TechnologyService.DeleteAsync(id);

			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
