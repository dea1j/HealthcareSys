using MediatR;
using DoctorService.Application.DTOs;

namespace DoctorService.Application.Queries.GetDoctorsBySpecialization;

public record GetDoctorsBySpecializationQuery(string Specialization) : IRequest<IEnumerable<DoctorDto>>;