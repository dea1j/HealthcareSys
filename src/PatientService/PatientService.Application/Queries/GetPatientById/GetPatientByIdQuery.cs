using MediatR;
using PatientService.Application.DTOs;

namespace PatientService.Application.Queries.GetPatientById;

public record GetPatientByIdQuery(Guid Id) : IRequest<PatientDto?>;
