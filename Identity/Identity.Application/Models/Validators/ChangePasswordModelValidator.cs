using FluentValidation;

namespace Identity.Application.Models.Validators
{
    public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            RuleFor(reg => reg.Email).NotEmpty().EmailAddress();
            RuleFor(reg => reg.Password).NotEmpty().MinimumLength(6);
            RuleFor(reg => reg.NewPassword).NotEmpty().MinimumLength(6);
        }
    }
}
