using DoctorService.Domain.Entities;
using DoctorService.Domain.Repositories;
using MediatR;

namespace DoctorService.Application.Commands.AddAvailabilitySlot;

public class AddAvailabilitySlotCommandHandler : IRequestHandler<AddAvailabilitySlotCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAvailabilitySlotRepository _slotRepository;

    public AddAvailabilitySlotCommandHandler(
        IDoctorRepository doctorRepository,
        IAvailabilitySlotRepository slotRepository)
    {
        _doctorRepository = doctorRepository;
        _slotRepository = slotRepository;
    }

    public async Task<Guid> Handle(AddAvailabilitySlotCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
            throw new KeyNotFoundException($"Doctor with ID {request.DoctorId} not found");
        
        var startTime = request.StartTime.Kind == DateTimeKind.Utc
            ? request.StartTime
            : DateTime.SpecifyKind(request.StartTime, DateTimeKind.Utc);

        var endTime = request.EndTime.Kind == DateTimeKind.Utc
            ? request.EndTime
            : DateTime.SpecifyKind(request.EndTime, DateTimeKind.Utc);
        
        var slot = AvailabilitySlot.Create(request.DoctorId, startTime, endTime);
        
        doctor.AddAvailabilitySlot(slot);
        await _slotRepository.AddAsync(slot, cancellationToken);

        return slot.Id;
    }
}