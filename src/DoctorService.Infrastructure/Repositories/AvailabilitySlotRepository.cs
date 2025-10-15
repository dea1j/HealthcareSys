using Microsoft.EntityFrameworkCore;
using DoctorService.Domain.Entities;
using DoctorService.Domain.Repositories;
using DoctorService.Infrastructure.Persistence;

namespace DoctorService.Infrastructure.Repositories;

public class AvailabilitySlotRepository : IAvailabilitySlotRepository
{
    private readonly ApplicationDbContext _context;

    public AvailabilitySlotRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AvailabilitySlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .AsNoTracking()
            .Where(s => s.DoctorId == doctorId)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsAsync(
        Guid doctorId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .AsNoTracking()
            .Where(s => s.DoctorId == doctorId &&
                       s.IsAvailable &&
                       s.StartTime >= startDate &&
                       s.EndTime <= endDate)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<AvailabilitySlot> AddAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        await _context.AvailabilitySlots.AddAsync(slot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return slot;
    }

    public async Task UpdateAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        _context.AvailabilitySlots.Update(slot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var slot = await _context.AvailabilitySlots
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            
        if (slot is not null)
        {
            _context.AvailabilitySlots.Remove(slot);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}