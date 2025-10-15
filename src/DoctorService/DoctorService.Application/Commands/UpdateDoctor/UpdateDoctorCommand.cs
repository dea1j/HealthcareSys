using MediatR;

namespace DoctorService.Application.Commands.UpdateDoctor;

public record UpdateDoctorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Specialization,
    int YearsOfExperience
) : IRequest<Unit>;