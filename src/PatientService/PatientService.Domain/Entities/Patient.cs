using PatientService.Domain.ValueObjects;

namespace PatientService.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public DateTime DateOfBirth { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    public string FullName => $"{FirstName} {LastName}";
    public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;
    
    private Patient() { }
    
    public static Patient Create(
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        DateTime dateOfBirth,
        string address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));
        
        // Ensure DateOfBirth is in UTC
        var utcDateOfBirth = dateOfBirth.Kind == DateTimeKind.Utc 
            ? dateOfBirth 
            : DateTime.SpecifyKind(dateOfBirth.Date, DateTimeKind.Utc);

        return new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = utcDateOfBirth,
            Address = address,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public void Update(
        string firstName,
        string lastName,
        PhoneNumber phoneNumber,
        string address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));
        
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }
}