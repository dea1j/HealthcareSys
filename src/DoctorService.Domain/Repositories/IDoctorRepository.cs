using DoctorService.Domain.Entities;

namespace DoctorService.Domain.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}