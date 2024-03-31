using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Api.Validators;

public class AnswerRequestDtoValidator : AbstractValidator<AnswerRequestDto>
{
    public AnswerRequestDtoValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0).WithMessage("QuestionId must be greater than 0");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(5, 500).WithMessage("Explanation must be between 5 and 500 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");
    }
}
