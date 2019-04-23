using FluentValidation;

namespace Identity.Application.Models.Validators
{
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(reg => reg.Email).NotEmpty().EmailAddress();
            RuleFor(reg => reg.Password).NotEmpty().MinimumLength(6);
        }
    }
}
