using MediatR;
using PatientService.Domain.Repositories;
using PatientService.Domain.ValueObjects;

namespace PatientService.Application.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id);
        if(patient is null)
            throw new KeyNotFoundException($"Patient with ID {request.Id} not found");
        
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        
        patient.Update(
            request.FirstName,
            request.LastName,
            phoneNumber,
            request.Address
        );

        await _patientRepository.UpdateAsync(patient);
        return Unit.Value;
    }
}