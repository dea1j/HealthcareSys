namespace DoctorService.API.Models;

public record CreateDoctorRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string LicenseNumber,
    int YearsOfExperience
);