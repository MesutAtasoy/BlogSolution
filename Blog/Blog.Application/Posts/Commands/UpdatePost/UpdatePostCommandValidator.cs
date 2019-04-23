using FluentValidation;

namespace Blog.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>, ICommandValidator
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.PostId).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(250).NotEmpty();
            RuleFor(x => x.PostContent).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
        }
    }
}
