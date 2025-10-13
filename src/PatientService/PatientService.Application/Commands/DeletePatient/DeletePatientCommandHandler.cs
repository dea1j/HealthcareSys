using MediatR;
using PatientService.Domain.Repositories;

namespace PatientService.Application.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;

    public DeletePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (patient is null)
            throw new KeyNotFoundException($"Patient with ID {request.Id} not found");

        await _patientRepository.DeleteAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}