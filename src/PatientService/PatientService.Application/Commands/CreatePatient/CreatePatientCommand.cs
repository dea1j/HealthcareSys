using MediatR;

namespace PatientService.Application.Commands.CreatePatient;

public record CreatePatientCommand(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber,
  DateTime DateOfBirth,
  string Address
) :  IRequest<Guid>;