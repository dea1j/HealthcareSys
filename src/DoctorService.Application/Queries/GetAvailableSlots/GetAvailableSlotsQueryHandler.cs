using DoctorService.Application.DTOs;
using DoctorService.Domain.Repositories;
using Mapster;
using MediatR;

namespace DoctorService.Application.Queries.GetAvailableSlots;

public class GetAvailableSlotsQueryHandler : IRequestHandler<GetAvailableSlotsQuery, IEnumerable<AvailabilitySlotDto>>
{
    private readonly IAvailabilitySlotRepository _slotRepository;

    public GetAvailableSlotsQueryHandler(IAvailabilitySlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<IEnumerable<AvailabilitySlotDto>> Handle(GetAvailableSlotsQuery request,
        CancellationToken cancellationToken)
    {
        var startDate = request.StartDate.Kind == DateTimeKind.Utc
            ? request.StartDate
            : DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);

        var endDate = request.EndDate.Kind == DateTimeKind.Utc
            ? request.EndDate
            : DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        var slots = await _slotRepository.GetAvailableSlotsAsync(
            request.DoctorId,
            startDate,
            endDate,
            cancellationToken);

        return slots.Adapt<IEnumerable<AvailabilitySlotDto>>();
    }
}