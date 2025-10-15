using MediatR;

namespace DoctorService.Application.Commands.DeleteDoctor;

public record DeleteDoctorCommand(Guid Id) : IRequest<Unit>;