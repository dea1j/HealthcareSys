using DoctorService.Domain.Entities;
using DoctorService.Domain.Repositories;
using DoctorService.Domain.ValueObjects;
using MediatR;

namespace DoctorService.Application.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;

    public CreateDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Guid> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var existingDoctor = await _doctorRepository.GetByEmailAsync(request.Email);
        if(existingDoctor is not null)
            throw new InvalidOperationException($"Doctor with email {request.Email} already exists");
        
        var email = Email.Create(request.Email);
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var doctor = Doctor.Create(
            request.FirstName,
            request.LastName,
            email,
            phoneNumber,
            request.Specialization,
            request.LicenseNumber,
            request.YearsOfExperience
        );
        await _doctorRepository.AddAsync(doctor, cancellationToken);

        return doctor.Id;
    }
}