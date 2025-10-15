namespace DoctorService.API.Models;

public record AddAvailabilitySlotRequest(
    DateTime StartTime,
    DateTime EndTime
);