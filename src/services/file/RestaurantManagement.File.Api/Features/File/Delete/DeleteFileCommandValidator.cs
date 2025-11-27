using FluentValidation;

namespace RestaurantManagement.File.Api.Features.File.Delete
{
    public class DeleteFileCommandValidator: AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
