using MediatR;
using DoctorService.Application.DTOs;

namespace DoctorService.Application.Queries.GetAvailableSlots;

public record GetAvailableSlotsQuery(
    Guid DoctorId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<IEnumerable<AvailabilitySlotDto>>;