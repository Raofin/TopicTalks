using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .IsInEnum().WithMessage("Invalid Role.");
    }
}
