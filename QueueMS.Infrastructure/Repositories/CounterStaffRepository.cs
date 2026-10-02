
using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure.Repositories;

public class CounterStaffRepository : ICounterStaffRepository
{
    private readonly QueueMSDatabaseContext _context;

    public CounterStaffRepository(QueueMSDatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool>ExistsAsync(int userId, int counterId)
    {
        return await _context.CounterStaffs
            .AnyAsync(x => x.StaffId == userId && x.CounterId == counterId);
    }

    public async Task AddAsync(CounterStaff counterStaff)
    {
        await _context.CounterStaffs.AddAsync(counterStaff);
        await _context.SaveChangesAsync();
    }
}
