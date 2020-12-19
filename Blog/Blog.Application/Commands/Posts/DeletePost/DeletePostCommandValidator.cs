using FluentValidation;

namespace Blog.Application.Commands.Posts.DeletePost
{
    public class UpdatePostCommandValidator : AbstractValidator<DeletePostCommand>, ICommandValidator
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.PostId).NotEmpty();
        }
    }
}
