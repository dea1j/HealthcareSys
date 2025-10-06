using MediatR;
using PatientService.Application.DTOs;

namespace PatientService.Application.Queries.GetAllPatients;

public record GetAllPatientsQuery : IRequest<IEnumerable<PatientDto>>;