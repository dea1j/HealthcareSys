using Mapster;
using MediatR;
using PatientService.Application.DTOs;
using PatientService.Domain.Repositories;

namespace PatientService.Application.Queries.GetAllPatients;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly IPatientRepository _patientRepository;
    
    public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(cancellationToken);
        return patients.Adapt<IEnumerable<PatientDto>>();
    }
}