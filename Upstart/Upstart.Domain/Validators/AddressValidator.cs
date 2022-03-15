using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upstart.Domain.VO;

namespace Upstart.Domain.Validators
{
    public class AddressValidator : AbstractValidator<AddressVO>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The '{PropertyName}' filed is required.")
                .NotEmpty().WithMessage("The '{PropertyName}' filed can't be empty.");
            
            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The '{PropertyName}' filed is required.")
                .NotEmpty().WithMessage("The '{PropertyName}' filed can't be empty.");

            RuleFor(x => x.State)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The '{PropertyName}' filed is required.")
                .NotEmpty().WithMessage("The '{PropertyName}' filed can't be empty.")
                .Length(2).WithMessage("The '{PropertyName}' field must be {MaxLength} characters long.");
        }
    }
}
