using FluentValidation;
using Pacogroup.Ecommerce.Application.DTO;

namespace Pacogroup.Ecommerce.Application.Validator
{
    public class SingUpDTOValidator : AbstractValidator<SingUpDTO>
    {
        public SingUpDTOValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            RuleFor(u => u.LastName).NotNull().NotEmpty();
            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty().Length(10, 250);
        }
    }
}