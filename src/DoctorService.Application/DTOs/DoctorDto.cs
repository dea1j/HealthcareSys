namespace DoctorService.Application.DTOs;

public record DoctorDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Specialization { get; init; } = string.Empty;
    public string LicenseNumber { get; init; } = string.Empty;
    public int YearsOfExperience { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}