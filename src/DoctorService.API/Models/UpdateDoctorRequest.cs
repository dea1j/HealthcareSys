namespace DoctorService.API.Models;

public record UpdateDoctorRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Specialization,
    int YearsOfExperience
);