using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories;

public interface IPatientRepository
{
    Task <Patient> GetByIdAsync(Guid Id, CancellationToken cancellationToken =  default);
    Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}