using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Cannot be null")
                .NotEmpty().WithMessage("Cannot be empty");
            RuleFor(x => x.Email).EmailAddress().NotNull().WithMessage("Cannot be null")
                .NotEmpty().WithMessage("Cannot be empty");
        }
    }
}
