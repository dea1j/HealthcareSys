namespace DoctorService.Domain.Entities;

public class AvailabilitySlot
{
    public Guid Id { get; private set; }
    public Guid DoctorId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Doctor Doctor { get; private set; } = null!;
    
    public int DurationInMinutes => (int)(EndTime - StartTime).TotalMinutes;
    
    private AvailabilitySlot() { }

    public static AvailabilitySlot Create(
        Guid doctorId,
        DateTime startTime,
        DateTime endTime)
    {
        if (startTime.Kind != DateTimeKind.Utc)
            throw new ArgumentException("Start time must be in UTC", nameof(startTime));

        if (endTime.Kind != DateTimeKind.Utc)
            throw new ArgumentException("End time must be in UTC", nameof(endTime));

        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");

        if (startTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot create slots in the past");

        return new AvailabilitySlot
        {
            Id = Guid.NewGuid(),
            DoctorId = doctorId,
            StartTime = startTime,
            EndTime = endTime,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public void MarkAsBooked()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Slot is already booked");

        IsAvailable = false;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
}