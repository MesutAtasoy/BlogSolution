using FluentValidation;

namespace Identity.Application.Models.Validators
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(reg => reg.Username).NotEmpty().MinimumLength(3);
            RuleFor(reg => reg.Name).NotEmpty();
            RuleFor(reg => reg.Email).NotEmpty().EmailAddress();
            RuleFor(reg => reg.Password).NotEmpty().MinimumLength(6);
        }
    }
}
