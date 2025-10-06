namespace PatientService.API.Models;

public record UpdatePatientRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Address
);