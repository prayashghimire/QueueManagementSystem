
using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.Interfaces;
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
        return await _context.Services.Where(x => x.IsActive).ToListAsync();
    }

    public async Task<Services?> GetByIdAsync(int id)
    {
        return await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Services service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }
}
