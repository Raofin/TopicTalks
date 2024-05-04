using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TopicTalks.Application.Validators;

public class FileUploadValidator : AbstractValidator<IFormFile>
{
    public FileUploadValidator()
    {
        RuleFor(x => x.Length)
            .GreaterThan(0)
            .WithMessage("The file cannot be empty.");

        RuleFor(x => x.Length)
            .NotNull()
            .LessThanOrEqualTo(2097152) // 2MB
            .WithMessage("File size cannot exceed 2MB.");

        RuleFor(x => x.ContentType)
            .Must(BeAnImage)
            .WithMessage("Only image files are allowed.");
    }

    private static bool BeAnImage(string contentType)
    {
        return contentType.StartsWith("image/");
    }
}