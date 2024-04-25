using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(@"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/")
            .WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(4, 20).WithMessage("Password must be between 4 and 20 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .IsInEnum().WithMessage("Invalid Role.");

        // Conditional validation for UserDetailDto
        When(x => x.UserDetails != null, () =>
        {
            RuleFor(x => x.UserDetails!.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");

            RuleFor(x => x.UserDetails!.InstituteName)
                .NotEmpty().WithMessage("Institute Name is required.")
                .Length(3, 50).WithMessage("Institute Name must be between 3 and 50 characters.");

            RuleFor(x => x.UserDetails!.IdCardNumber)
                .NotEmpty().WithMessage("Id Card Number is required.")
                .Length(6, 20).WithMessage("Id Card Number must be between 6 and 20 characters.");
        });
    }
}
