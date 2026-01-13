using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.Exceptions;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Academy.Application.Services.AcademyServices
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager
        , IConfiguration _configuration, IEmailSender _emailSender) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email) ?? throw new UserNotFoundException(loginDTO.Email);

            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (IsPasswordValid)
                return new UserDTO()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            else
                throw new UnAuthorizedException();
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber
            };

            var Result = await _userManager.CreateAsync(User, registerDTO.Password);
            if (Result.Succeeded)
                return new UserDTO()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        //public async Task ForgotPasswordAsync(ForgotPasswordDTO dto)
        //{
        //	var user = await _userManager.FindByEmailAsync(dto.Email);

        //	// مهم: ما تفضحش وجود الإيميل من عدمه
        //	if (user is null)
        //		return;

        //	var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //	// URL-safe
        //	var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        //	// اللينك ده خليّه يروح لفرونت-إند صفحة reset (أفضل)
        //	// مثال: https://your-frontend/reset-password?email=...&token=...
        //	var frontUrl = _configuration["FrontEnd:ResetPasswordUrl"];
        //	var resetLink = $"{frontUrl}?email={Uri.EscapeDataString(user.Email!)}&token={tokenEncoded}";

        //	var subject = "Reset your password";
        //	var body = $"<p>Click to reset your password:</p><p><a href='{resetLink}'>Reset Password</a></p>";

        //	await _emailSender.SendAsync(user.Email!, subject, body);
        //}

        //public async Task ResetPasswordAsync(ResetPasswordDTO dto)
        //{
        //	var user = await _userManager.FindByEmailAsync(dto.Email);
        //	if (user is null)
        //	{
        //		// برضه ممكن ترجع BadRequest عام (أو تعمل نفس مبدأ عدم الإفصاح)
        //		throw new BadRequestException(new List<string> { "Invalid reset request." });
        //	}

        //	// Decode token
        //	string token;
        //	try
        //	{
        //		var bytes = WebEncoders.Base64UrlDecode(dto.Token);
        //		token = Encoding.UTF8.GetString(bytes);
        //	}
        //	catch
        //	{
        //		throw new BadRequestException(new List<string> { "Invalid token format." });
        //	}

        //	var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

        //	if (!result.Succeeded)
        //	{
        //		var errors = result.Errors.Select(e => e.Description).ToList();
        //		throw new BadRequestException(errors);
        //	}
        //}


        public async Task<string?> GenerateResetTokenAsync(ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            // ما تفضحش وجود الإيميل
            if (user is null) return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token; // ✅ رجّعه خام (raw)
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw new BadRequestException(new List<string> { "Invalid reset request." });

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
            (
                issuer: _configuration.GetSection("JWTOptions")["ISSuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
