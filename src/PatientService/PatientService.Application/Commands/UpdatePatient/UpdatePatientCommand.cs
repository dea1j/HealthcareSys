using MediatR;

namespace PatientService.Application.Commands.UpdatePatient;

public record UpdatePatientCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Address
) : IRequest<Unit>;