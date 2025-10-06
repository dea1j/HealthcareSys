namespace PatientService.Application.DTOs;

public class PatientDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}