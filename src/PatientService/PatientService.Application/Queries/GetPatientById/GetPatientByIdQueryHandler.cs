using Mapster;
using MediatR;
using PatientService.Application.DTOs;
using PatientService.Domain.Repositories;

namespace PatientService.Application.Queries.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto?>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);
        return patient?.Adapt<PatientDto>();
    }
}