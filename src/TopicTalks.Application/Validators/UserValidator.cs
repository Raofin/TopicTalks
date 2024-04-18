using FluentValidation;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Validators;

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

public class PasswordChangeValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old Password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New Password is required.")
            .Length(4, 20).WithMessage("New Password must be between 4 and 20 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.NewPassword).WithMessage("The new password and confirmation password do not match.");
    }
}
