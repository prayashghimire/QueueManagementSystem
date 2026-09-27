
using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure.Repositories;

public class CounterRepository : ICounterRepository
{
    private readonly QueueMSDatabaseContext _context;

    public CounterRepository(QueueMSDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Counter?>GetByIdAsync(int id)
    {
        return await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Counter>> GetActiveCounterAsync()
    {
        return await _context.Counters.Where(c => c.isActive).ToListAsync();
    }

    public async Task<List<Counter>>GetCounterForServiceAsync(int serviceId)
    {
        return await _context.Counters
            .Where(c => c.isActive && c.CounterServices.Any(c => c.ServiceId == serviceId)).ToListAsync();
    }

    public async Task<Counter?> GetHighestWorkloadCounterAsync()
    {
        return await _context.Counters
            .Include(c => c.Tokens)
            .OrderByDescending(c => c.Tokens.Count(t => t.Status == TokenStatus.WAITING))
            .FirstOrDefaultAsync();
    }
    public async Task AddAsync(Counter counter)
    {
        await _context.Counters.AddAsync(counter);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Counter counter)
    {
        _context.Counters.Update(counter);
        await _context.SaveChangesAsync();
    }
}
