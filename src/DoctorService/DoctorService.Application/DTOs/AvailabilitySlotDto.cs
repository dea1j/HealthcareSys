namespace DoctorService.Application.DTOs;

public record AvailabilitySlotDto
{
    public Guid Id { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public bool IsAvailable { get; init; }
    public int DurationInMinutes { get; init; }
    public DateTime CreatedAt { get; init; }
}