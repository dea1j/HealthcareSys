using DoctorService.Domain.Repositories;
using MediatR;

namespace DoctorService.Application.Commands.BookSlot;

public class BookSlotCommandHandler : IRequestHandler<BookSlotCommand, Unit>
{
    private readonly IAvailabilitySlotRepository _slotRepository;

    public BookSlotCommandHandler(IAvailabilitySlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<Unit> Handle(BookSlotCommand request, CancellationToken cancellationToken)
    {
        var slot = await _slotRepository.GetByIdAsync(request.SlotId,  cancellationToken);
        if (slot is null)
            throw new KeyNotFoundException($"Slot with ID {request.SlotId} not found");
        
        slot.MarkAsBooked();
        await _slotRepository.UpdateAsync(slot, cancellationToken);

        return Unit.Value;
    }
}