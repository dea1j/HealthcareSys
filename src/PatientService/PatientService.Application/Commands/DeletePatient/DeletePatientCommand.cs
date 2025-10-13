using MediatR;

namespace PatientService.Application.Commands.DeletePatient;

public record DeletePatientCommand(Guid Id) : IRequest<Unit>;