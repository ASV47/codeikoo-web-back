using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class ContactMessagesController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpPost]
		public async Task<ActionResult<ContactMessageDto>> Add([FromBody] CreateContactMessageDto dto)
		{
			var ContactMsg = await _serviceManager.ContactMessageService.AddAsync(dto);
			return Ok(ContactMsg);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var result = await _serviceManager.ContactMessageService.DeleteAsync(id);
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetAll()
		{
			var Messages = await _serviceManager.ContactMessageService.GetAllAsync();
			return Ok(Messages);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ContactMessageDto?>> GetById(int id)
		=> Ok(await _serviceManager.ContactMessageService.GetByIdAsync(id));


		[HttpPut("{id}/restore")]
		public async Task<ActionResult<bool>> Restore(int id)
		{
			var result = await _serviceManager.ContactMessageService.RestoreAsync(id);
			return Ok(result);
		}

	}
}
