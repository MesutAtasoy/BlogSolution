using FluentValidation;

namespace Blog.Application.Posts.Commands.DeletePost
{
    public class UpdatePostCommandValidator : AbstractValidator<DeletePostCommand>, ICommandValidator
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.PostId).NotEmpty();
        }
    }
}
