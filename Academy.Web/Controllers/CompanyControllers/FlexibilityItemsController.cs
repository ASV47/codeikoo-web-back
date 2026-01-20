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
	public class FlexibilityItemsController(IServiceManager _serviceManager, IWebHostEnvironment env) : APIBaseController
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Create([FromForm] CreateFlexibilityItemDto dto)
        {
            await _serviceManager.flexibilityItemService.AddAsync(dto.Title, dto.Icon);
            return Ok(new { Message = "Created Successfully" });
        }


        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateFlexibilityItemDto dto)
        {
            var ok = await _serviceManager.flexibilityItemService.UpdateAsync(id, dto.Title, dto.Icon);

            return ok ? Ok(new { Message = "Updated Successfully" }) : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _serviceManager.flexibilityItemService.DeleteAsync(id);

            return ok ? Ok(new { Message = "Deleted Successfully" }) : NotFound();
        }

    }
}
