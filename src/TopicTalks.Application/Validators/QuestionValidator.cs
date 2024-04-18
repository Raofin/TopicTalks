using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Validators;

public class QuestionCreateValidator : AbstractValidator<QuestionCreateDto>
{
    public QuestionCreateValidator()
    {
        RuleFor(x => x.Topic)
            .NotEmpty().WithMessage("Topic is required")
            .Length(5, 200).WithMessage("Topic must be between 5 and 200 characters");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(10, 9999).WithMessage("Explanation must be between 10 and 9999 characters");
    }
}

public class QuestionUpdateValidator : AbstractValidator<QuestionUpdateDto>
{
    public QuestionUpdateValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0).WithMessage("QuestionId is required");

        RuleFor(x => x.Topic)
            .NotEmpty().WithMessage("Topic is required")
            .Length(5, 200).WithMessage("Topic must be between 5 and 200 characters");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(10, 9999).WithMessage("Explanation must be between 5 and 9999 characters");
    }
}
