using DoctorService.Domain.Entities;

namespace DoctorService.Domain.Repositories;

public interface IAvailabilitySlotRepository
{
    Task<AvailabilitySlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AvailabilitySlot>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsAsync(Guid doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<AvailabilitySlot> AddAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default);
    Task UpdateAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}