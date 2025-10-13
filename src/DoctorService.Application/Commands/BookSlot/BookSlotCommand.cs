using MediatR;

namespace DoctorService.Application.Commands.BookSlot;

public record BookSlotCommand(Guid SlotId) : IRequest<Unit>;