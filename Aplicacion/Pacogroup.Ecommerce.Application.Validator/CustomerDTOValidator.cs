using FluentValidation;
using Pacogroup.Ecommerce.Application.DTO;

namespace Pacogroup.Ecommerce.Application.Validator
{
    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty().NotNull().Length(5, 40);
            RuleFor(c => c.ContactName).NotEmpty().NotNull().Length(5, 30);
            RuleFor(c => c.ContactTitle).NotEmpty().NotNull().Length(5, 30);
            RuleFor(c => c.Address).NotEmpty().NotNull().Length(5, 60);
            RuleFor(c => c.City).NotEmpty().NotNull().Length(2, 15);
            RuleFor(c => c.Region).NotEmpty().NotNull().Length(2, 15);
            RuleFor(c => c.PostalCode).NotEmpty().NotNull().Length(2, 10);
            RuleFor(c => c.Country).NotEmpty().NotNull().Length(2, 15);
            RuleFor(c => c.Phone).NotEmpty().NotNull().Length(5, 24);
            RuleFor(c => c.Fax).NotEmpty().NotNull().Length(5, 24);
        }
    }
}