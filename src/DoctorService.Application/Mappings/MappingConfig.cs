using DoctorService.Application.DTOs;
using DoctorService.Domain.Entities;
using Mapster;

namespace DoctorService.Application.Mappings;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Doctor, DoctorDto>.NewConfig()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.FullName, src => src.FullName);
        
        TypeAdapterConfig<AvailabilitySlot, AvailabilitySlotDto>.NewConfig()
            .Map(dest => dest.DurationInMinutes, src => src.DurationInMinutes);

        TypeAdapterConfig<Doctor, DoctorWithSlotsDto>.NewConfig()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.AvailableSlots, src => src.AvailabilitySlots
                .Where(s => s.IsAvailable)
                .OrderBy(s => s.StartTime));
    }
}