using MediatR;
using DoctorService.Application.DTOs;

namespace DoctorService.Application.Queries.GetAllDoctors;

public record GetAllDoctorsQuery : IRequest<IEnumerable<DoctorDto>>;