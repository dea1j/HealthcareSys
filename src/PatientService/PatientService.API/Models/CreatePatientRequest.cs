namespace PatientService.API.Models;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Address
);