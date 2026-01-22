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
	public class ClientsController(IServiceManager _serviceManager, IWebHostEnvironment env) : APIBaseController
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Add([FromForm] CreateClientDto dto)
        {
            await _serviceManager.clientService.AddAsync(dto.Image);
            return Ok(new { Message = "Client Logo Added Successfully" });
        }


        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateClientDto dto)
        {
            var result = await _serviceManager.clientService.UpdateAsync(id, dto.Image);

            return result
                ? Ok(new { Message = "Client Logo Updated Successfully" })
                : NotFound(new { Message = "Client not found" });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _serviceManager.clientService.DeleteAsync(id);

            return ok
                ? Ok(new { Message = "Deleted Successfully" })
                : NotFound();
        }

    }
}
