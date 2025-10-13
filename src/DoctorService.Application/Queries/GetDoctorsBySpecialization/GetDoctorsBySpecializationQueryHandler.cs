using Mapster;
using MediatR;
using DoctorService.Application.DTOs;
using DoctorService.Domain.Repositories;

namespace DoctorService.Application.Queries.GetDoctorsBySpecialization;

public class GetDoctorsBySpecializationQueryHandler : IRequestHandler<GetDoctorsBySpecializationQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsBySpecializationQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsBySpecializationQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _doctorRepository.GetBySpecializationAsync(request.Specialization, cancellationToken);
        return doctors.Adapt<IEnumerable<DoctorDto>>();
    }
}