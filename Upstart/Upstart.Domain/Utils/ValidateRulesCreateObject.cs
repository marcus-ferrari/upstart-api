using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using Upstart.Domain.Extensions;

namespace Upstart.Domain.Utils
{
    public class ValidateRulesCreateObject<T> where T : class
    {

        public void Validate(AbstractValidator<T> addressValidator, T objectToValidate)
        {
            ValidationResult validationResult = addressValidator.Validate(objectToValidate);

            if (!validationResult.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var failure in validationResult.Errors)
                {
                    errors.Add(failure.ErrorMessage);
                }
                throw new CustomExceptionErrorList(errors);
            }
        }
    }
}
