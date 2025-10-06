using MediatR;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;
using PatientService.Domain.ValueObjects;

namespace PatientService.Application.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await _patientRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingPatient != null)
            throw new InvalidOperationException($"Patient with email {request.Email} already exists");
        
        var email = Email.Create(request.Email);
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var patient = Patient.Create(
            request.FirstName,
            request.LastName,
            email,
            phoneNumber,
            request.DateOfBirth,
            request.Address
        );

        await _patientRepository.AddAsync(patient, cancellationToken);
        return patient.Id;
    }
}