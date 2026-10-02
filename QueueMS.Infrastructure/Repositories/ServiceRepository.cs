
using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.ServiceModel;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly QueueMSDatabaseContext _context;

    public ServiceRepository(QueueMSDatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Services service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var service = await GetByIdAsync(id);

        if (service == null) return;

        service.IsActive = false;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Services>> GetAllAsync()
    {
        return await _context.Services
            .Where(x => x.IsActive).ToListAsync();
    }

    public async Task<Services?> GetByIdAsync(int id)
    {
        return await _context.Services
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Services?> GetByNameAsync(string name)
    {
        return await _context.Services
            .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
    }

    public async Task<double>GetAverageServiceTimeAsync(int id)
    {
        var duration =  await _context.Tokens
            .Where(t => t.ServiceId == id &&
                t.Status == TokenStatus.COMPLETED &&
                t.CalledAt != null &&
                t.CompletedAt != null)
            .Select(t => new
            {
                t.CalledAt,
                t.CompletedAt
            })
            .ToListAsync();

        if (duration.Count == 0)
            return 0;

        var average = duration.Average(t => 
            (t.CalledAt!.Value - t.CompletedAt!.Value).TotalSeconds);

        return average / 60;
    }

    public async Task<Dictionary<int, double>> GetAverageServiceTimeAsync()
    {
        return await _context.Tokens
            .Where(t =>
                t.Status == TokenStatus.COMPLETED &&
                t.CalledAt != null &&
                t.CompletedAt != null)
                .GroupBy(t => t.ServiceId)
            .Select(g => new
            {
                ServiceId = g.Key,
                AverageSeconds = g.Average(t => (t.CompletedAt!.Value - t.CalledAt!.Value).TotalSeconds)
            })
            .ToDictionaryAsync(
                x => x.ServiceId,
                x => x.AverageSeconds / 60.0
            );
            
    }


    public async Task UpdateAsync(Services service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }
}
