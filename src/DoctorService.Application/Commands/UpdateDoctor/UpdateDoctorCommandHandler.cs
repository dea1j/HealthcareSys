using DoctorService.Domain.Repositories;
using DoctorService.Domain.ValueObjects;
using MediatR;

namespace DoctorService.Application.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
{
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);
        if (doctor is null)
            throw new KeyNotFoundException($"Doctor with ID {request.Id} not found");

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        doctor.Update(
            request.FirstName,
            request.LastName,
            phoneNumber,
            request.Specialization,
            request.YearsOfExperience
        );

        await _doctorRepository.UpdateAsync(doctor, cancellationToken);

        return Unit.Value;
    }
}