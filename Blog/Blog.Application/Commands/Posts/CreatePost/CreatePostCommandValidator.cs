using FluentValidation;

namespace Blog.Application.Commands.Posts.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand> , ICommandValidator
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(250).NotEmpty();
            RuleFor(x => x.PostContent).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
        }
    }
}
