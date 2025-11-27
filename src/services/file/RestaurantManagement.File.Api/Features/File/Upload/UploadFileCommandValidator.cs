using FluentValidation;

namespace RestaurantManagement.File.Api.Features.File.Upload
{
    public class UploadFileCommandValidator: AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.File).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
