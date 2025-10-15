namespace DoctorService.Application.DTOs;

public record DoctorWithSlotsDto 
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Specialization { get; init; } = string.Empty;
    public List<AvailabilitySlotDto> AvailableSlots { get; init; } = new();
}