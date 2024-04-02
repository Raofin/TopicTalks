using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Api.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            // https://stackoverflow.com/a/201378/15324456
            .Matches(@"^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$")
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
