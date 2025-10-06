using FluentValidation;

namespace PatientService.Application.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+?[\d\s\-\(\)]+$").WithMessage("Invalid phone number format");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required");
    }
}