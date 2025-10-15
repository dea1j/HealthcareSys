using MediatR;
using DoctorService.Application.DTOs;

namespace DoctorService.Application.Queries.GetDoctorById;

public record GetDoctorByIdQuery(Guid Id) : IRequest<DoctorDto?>;