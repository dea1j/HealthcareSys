using Mapster;
using PatientService.Application.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Application.Mappings;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Patient, PatientDto>.NewConfig()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Age, src => src.Age);
    }
    
}