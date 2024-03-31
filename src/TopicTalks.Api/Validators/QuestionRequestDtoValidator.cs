using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Api.Validators;

public class QuestionRequestDtoValidator : AbstractValidator<QuestionRequestDto>
{
    public QuestionRequestDtoValidator()
    {
        RuleFor(x => x.Topic)
            .NotEmpty().WithMessage("Topic is required")
            .Length(5, 100).WithMessage("Topic must be between 5 and 100 characters");

        RuleFor(x => x.Explanation)
            .NotEmpty().WithMessage("Explanation is required")
            .Length(5, 500).WithMessage("Explanation must be between 5 and 500 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");
    }
}
