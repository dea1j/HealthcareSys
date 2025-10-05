namespace PatientService.Domain.ValueObjects;

public sealed class PhoneNumber : IEquatable<PhoneNumber>
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty", nameof(value));

        // Remove common formatting characters
        var cleaned = new string(value.Where(char.IsDigit).ToArray());

        if (cleaned.Length < 10 || cleaned.Length > 15)
            throw new ArgumentException("Phone number must be between 10-15 digits", nameof(value));

        return new PhoneNumber(cleaned);
    }

    public bool Equals(PhoneNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PhoneNumber other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(PhoneNumber phone) => phone.Value;
}