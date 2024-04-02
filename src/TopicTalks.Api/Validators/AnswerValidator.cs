using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Api.Validators;

public class AnswerCreateValidator : AbstractValidator<AnswerCreateDto>
{
    public AnswerCreateValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0).WithMessage("QuestionId must be greater than 0");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(5, 2000).WithMessage("Explanation must be between 5 and 2000 characters");
    }
}

public class AnswerUpdateValidator : AbstractValidator<AnswerUpdateDto>
{
    public AnswerUpdateValidator()
    {
        RuleFor(x => x.AnswerId)
            .GreaterThan(0).WithMessage("AnswerId must be greater than 0");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(5, 2000).WithMessage("Explanation must be between 5 and 2000 characters");
    }
}
