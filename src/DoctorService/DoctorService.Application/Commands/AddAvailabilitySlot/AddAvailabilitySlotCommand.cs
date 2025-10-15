using MediatR;

namespace DoctorService.Application.Commands.AddAvailabilitySlot;

public record AddAvailabilitySlotCommand(
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime
) : IRequest<Guid>;