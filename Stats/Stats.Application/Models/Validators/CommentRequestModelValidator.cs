using FluentValidation;

namespace Stats.Application.Models.Validators
{
    public class CommentRequestModelValidator : AbstractValidator<CommentRequestModel>
    {
        public CommentRequestModelValidator()
        {
            RuleFor(reg => reg.PostId).NotEmpty();
            RuleFor(reg => reg.Comment).NotEmpty().MaximumLength(250);
        }
    }
}
