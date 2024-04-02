using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Api.Validators;

public class UserDetailsValidator : AbstractValidator<UserDetailDto>
{
    public UserDetailsValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");

        RuleFor(x => x.InstituteName)
            .NotEmpty().WithMessage("Institute Name is required.")
            .Length(3, 50).WithMessage("Institute Name must be between 3 and 50 characters.");

        RuleFor(x => x.IdCardNumber)
            .NotEmpty().WithMessage("Id Card Number is required.")
            .Length(6).WithMessage("Id Card Number must be 6 characters.");
    }
}