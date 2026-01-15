//using Academy.Infrastructure.Entities.AcademyEntities;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Academy.Infrastructure.Data
//{
//	public class IdentityDataSeeder
//	{
//		private readonly RoleManager<IdentityRole> _roleManager;
//		private readonly UserManager<ApplicationUser> _userManager;

//		public IdentityDataSeeder(
//			RoleManager<IdentityRole> roleManager,
//			UserManager<ApplicationUser> userManager)
//		{
//			_roleManager = roleManager;
//			_userManager = userManager;
//		}

//		public async Task IdentityDataSeedAsync()
//		{
//			// 1) Seed Roles (بالاسم مش Any)
//			var roles = new[] { "Admin", "SuperAdmin" };

//			foreach (var role in roles)
//			{
//				if (!await _roleManager.RoleExistsAsync(role))
//				{
//					var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
//					if (!roleResult.Succeeded)
//					{
//						var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
//						throw new Exception($"Failed to create role '{role}': {errors}");
//					}
//				}
//			}

//			// 2) Seed Users (بالايميل/اليوزر نيم)
//			await EnsureUserWithRoleAsync(
//				email: "ahmedkhaled01@gmail.com",
//				userName: "ahmedkhaled",
//				displayName: "Ahmed Khaled",
//				phoneNumber: "01234567890",
//				password: "P@ssw0rd",
//				role: "SuperAdmin"
//			);

//			await EnsureUserWithRoleAsync(
//				email: "ahmedamr02@gmail.com",
//				userName: "AhmedAmr",
//				displayName: "Ahmed Amr",
//				phoneNumber: "01145100263",
//				password: "Pa$$w0rd",
//				role: "Admin"
//			);
//		}

//		private async Task EnsureUserWithRoleAsync(
//			string email,
//			string userName,
//			string displayName,
//			string phoneNumber,
//			string password,
//			string role)
//		{
//			// جرّب تلاقيه بالايميل الأول
//			var user = await _userManager.FindByEmailAsync(email);

//			// لو مش موجود، أنشئه
//			if (user == null)
//			{
//				user = new ApplicationUser
//				{
//					Email = email,
//					UserName = userName,
//					DisplayName = displayName,
//					PhoneNumber = phoneNumber,
//					EmailConfirmed = true
//				};

//				var createResult = await _userManager.CreateAsync(user, password);
//				if (!createResult.Succeeded)
//				{
//					var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
//					throw new Exception($"Failed to create user '{email}': {errors}");
//				}
//			}

//			// ضيفه للـ Role لو مش مضاف
//			if (!await _userManager.IsInRoleAsync(user, role))
//			{
//				var addRoleResult = await _userManager.AddToRoleAsync(user, role);
//				if (!addRoleResult.Succeeded)
//				{
//					var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
//					throw new Exception($"Failed to add user '{email}' to role '{role}': {errors}");
//				}
//			}
//		}
//	}
//}
