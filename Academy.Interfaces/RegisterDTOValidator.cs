using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Invalid Email");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName Is Required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber Is Required")
                .Matches(@"^\d{10,15}$").WithMessage("Phone number must be digits only (10-15 digits).");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword is required")
                .Equal(x => x.Password).WithMessage("Password and ConfirmPassword do not match");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("DisplayName Is Required");

            RuleFor(x => x.Governorate)
                .NotEmpty().WithMessage("Governorate is required");
        }
    }
}
