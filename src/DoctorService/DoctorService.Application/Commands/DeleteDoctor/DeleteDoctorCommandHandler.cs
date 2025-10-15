using DoctorService.Domain.Repositories;
using MediatR;

namespace DoctorService.Application.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Unit>
{
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Unit> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (doctor is null)
            throw new KeyNotFoundException($"Doctor with ID {request.Id} not found");

        await _doctorRepository.DeleteAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}