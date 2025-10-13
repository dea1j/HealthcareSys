using MediatR;

namespace DoctorService.Application.Commands.CreateDoctor;

public record CreateDoctorCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string LicenseNumber,
    int YearsOfExperience
) : IRequest<Guid>;