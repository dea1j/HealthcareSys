using DoctorService.Application.DTOs;
using DoctorService.Domain.Repositories;
using Mapster;
using MediatR;

namespace DoctorService.Application.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto?>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorDto?> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);
        return doctor?.Adapt<DoctorDto>();
    }
}