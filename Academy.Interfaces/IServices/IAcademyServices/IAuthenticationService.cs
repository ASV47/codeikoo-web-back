using Academy.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IAuthenticationService
	{
		public Task<UserDTO> LoginAsync(LoginDTO loginDTO);
		public Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);

		//Task ForgotPasswordAsync(ForgotPasswordDTO dto);
		Task ResetPasswordAsync(ResetPasswordDTO dto);
		Task<string?> GenerateResetTokenAsync(ForgotPasswordDTO dto);

	}
}
