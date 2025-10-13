using FluentValidation;

namespace DoctorService.Application.Commands.AddAvailabilitySlot;

public class AddAvailabilitySlotCommandValidator : AbstractValidator<AddAvailabilitySlotCommand>
{
    public AddAvailabilitySlotCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required")
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start time cannot be in the past");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time");

        RuleFor(x => x)
            .Must(x => (x.EndTime - x.StartTime).TotalHours <= 8)
            .WithMessage("Slot duration cannot exceed 8 hours");
    }
}