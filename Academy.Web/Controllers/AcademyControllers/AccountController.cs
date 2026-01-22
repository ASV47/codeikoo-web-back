using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy.Web.Controllers
{
	[ApiExplorerSettings(GroupName = "Academy")]
	public class AccountController(IServiceManager _serviceManager,
		UserManager<ApplicationUser> _userManager) : APIBaseController
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

        [HttpGet]
        public async Task<ActionResult<List<UserListDto>>> GetAllUsers()
        {
            var users = await _userManager.Users
                .AsNoTracking()
                .ToListAsync();

            var result = new List<UserListDto>(users.Count);

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserListDto
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = roles
                });
            }

            return Ok(result);
        }

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
