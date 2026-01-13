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
	public class ClientsController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult> GetAll()
		=> Ok(await _serviceManager.clientService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<ClientDto>> GetById(int id)
		{
			var result = await _serviceManager.clientService.GetByIdAsync(id);

			if (result == null)
				return NotFound(new { Message = $"Client with ID {id} not found." });

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> Add([FromForm] CreateClientDto dto)
		{
			var path = DocumentSettings.UploadFile(dto.Image, "ClientsLogo");

			await _serviceManager.clientService.AddAsync(path);
			return Ok(new { Message = "Client Logo Added Successfully" });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromForm] CreateClientDto dto)
		{
			var existingClient = await _serviceManager.clientService.GetByIdAsync(id);
			if (existingClient == null) return NotFound(new { Message = "Client not found" });

			string? finalPath = null;

			if (dto.Image != null)
			{
				var oldPath = existingClient.LogoUrl.Replace("https://localhost:7048", "");
				DocumentSettings.DeleteFile(oldPath);

				finalPath = DocumentSettings.UploadFile(dto.Image, "ClientsLogo");
			}

			var result = await _serviceManager.clientService.UpdateAsync(id, finalPath);

			return result ? Ok(new { Message = "Client Logo Updated Successfully" }) : BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var clientDto = await _serviceManager.clientService.GetByIdAsync(id);
			if (clientDto == null) return NotFound();

			var relativePath = clientDto.LogoUrl.Replace("https://localhost:7048/", "");
			DocumentSettings.DeleteFile(relativePath);

			await _serviceManager.clientService.DeleteAsync(id);

			return Ok(new { Message = "Deleted Successfully" });
		}
	}
}
