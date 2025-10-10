using DoctorService.Domain.ValueObjects;

namespace DoctorService.Domain.Entities;

public class Doctor
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public string Specialization { get; private set; } = string.Empty;
    public string LicenseNumber { get; private set; } = string.Empty;
    public int YearsOfExperience { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    public string FullName => $"Dr. {FirstName} {LastName}";
    
    private readonly List<AvailabilitySlot> _availabilitySlots = new();
    public IReadOnlyCollection<AvailabilitySlot> AvailabilitySlots => _availabilitySlots.AsReadOnly();

    private Doctor() { }

    public static Doctor Create(
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        string specialization,
        string licenseNumber,
        int yearsOfExperience)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        if (string.IsNullOrWhiteSpace(specialization))
            throw new ArgumentException("Specialization is required", nameof(specialization));

        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new ArgumentException("License number is required", nameof(licenseNumber));

        if (yearsOfExperience < 0)
            throw new ArgumentException("Years of experience cannot be negative", nameof(yearsOfExperience));

        return new Doctor
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Specialization = specialization,
            LicenseNumber = licenseNumber,
            YearsOfExperience = yearsOfExperience,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public void Update(
        string firstName,
        string lastName,
        PhoneNumber phoneNumber,
        string specialization,
        int yearsOfExperience)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        if (string.IsNullOrWhiteSpace(specialization))
            throw new ArgumentException("Specialization is required", nameof(specialization));

        if (yearsOfExperience < 0)
            throw new ArgumentException("Years of experience cannot be negative", nameof(yearsOfExperience));

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Specialization = specialization;
        YearsOfExperience = yearsOfExperience;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAvailabilitySlot(AvailabilitySlot slot)
    {
        var hasOverlap = _availabilitySlots.Any(s =>
            s.StartTime < slot.EndTime && slot.StartTime < s.EndTime && s.IsAvailable);

        if (hasOverlap)
            throw new InvalidOperationException("Slot overlaps with existing availability");

        _availabilitySlots.Add(slot);
    }
}