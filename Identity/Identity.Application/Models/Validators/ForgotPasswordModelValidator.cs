using FluentValidation;

namespace Identity.Application.Models.Validators
{
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordRequestModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(reg => reg.Email).NotEmpty().EmailAddress();
        }
    }
}
