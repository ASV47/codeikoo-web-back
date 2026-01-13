using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class AccountController(IServiceManager _serviceManager) : APIBaseController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
		{
			var User = await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
			return Ok(User);
		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
		{
			var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
			return Ok(User);
		}

		//[HttpPost("forgot-password")]
		//[AllowAnonymous]
		//public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO dto)
		//{
		//	await _serviceManager.AuthenticationService.ForgotPasswordAsync(dto);

		//	// دايمًا 200
		//	return Ok(new { message = "If the email exists, a reset link has been sent." });
		//}

		//[HttpPost("reset-password")]
		//[AllowAnonymous]
		//public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
		//{
		//	await _serviceManager.AuthenticationService.ResetPasswordAsync(dto);
		//	return Ok(new { message = "Password has been reset successfully." });
		//}

		[HttpPost("forgot-password")]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO dto)
		{
			var token = await _serviceManager.AuthenticationService.GenerateResetTokenAsync(dto);

			// للاختبار: رجّع token (خليها مؤقتة بس)
			return Ok(new ForgotPasswordResponseDTO
			{
				Token = token
			});
		}

		[HttpPost("reset-password")]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
		{
			await _serviceManager.AuthenticationService.ResetPasswordAsync(dto);
			return Ok(new { message = "Password has been reset successfully." });
		}

	}
}
