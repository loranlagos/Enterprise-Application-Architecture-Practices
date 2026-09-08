using FluentValidation;
using Pacogroup.Ecommerce.Application.DTO;

namespace Pacogroup.Ecommerce.Application.Validator;

public class SingInDTOValidator : AbstractValidator<SingInDTO>
{
    public SingInDTOValidator()
    {
        RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(u => u.Password).NotNull().NotEmpty();
    }
}
