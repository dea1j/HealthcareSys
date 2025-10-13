using Mapster;
using MediatR;
using DoctorService.Application.DTOs;
using DoctorService.Domain.Repositories;

namespace DoctorService.Application.Queries.GetAllDoctors;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _doctorRepository.GetAllAsync(cancellationToken);
        return doctors.Adapt<IEnumerable<DoctorDto>>();
    }
}