
using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.DTOs.Counter;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.QueueModels;
using QueueMS.Domain.Models.TokenModels;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure.Repositories;

public class QueueRepository : IQueueRepository
{
    private readonly QueueMSDatabaseContext _context;

    public QueueRepository(QueueMSDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<CounterWorkloadDto>> GetCounterWorkloadAsync()
    {
        return await _context.Counters
            .Select(c => new CounterWorkloadDto{
                CounterId = c.Id,
                CounterName = c.Name,

                WaitingCustomerCount = c.Tokens.Count(t => t.Status == TokenStatus.WAITING),
                isActive = c.isActive
            })
            .OrderByDescending(x => x.WaitingCustomerCount)
            .ToListAsync();
    }
    public async Task<Token?>GetNextTokenAsync(int serviceId)
    {
        return await _context.Tokens
            .Include(t => t.Service)
            .Where(t => t.ServiceId == serviceId && 
            t.Status == TokenStatus.WAITING)
            .OrderBy(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<int?>CallNextTokenAsync(int counterId, int staffId, int serviceId)
    {
        var token = await _context.Tokens
            .Where(t => t.ServiceId == serviceId && t.Status == TokenStatus.WAITING)
            .OrderBy(t => t.CreatedAt)
            .FirstOrDefaultAsync();

        if (token == null) return null;

        token.CounterId = counterId;
        token.Status = TokenStatus.CALLED;
        token.CalledAt = DateTime.UtcNow;

        var history = new QueueHistory
        {
            TokenId = token.Id,
            CounterId =counterId,
            PerformedBy = staffId,
            Action = "CALLED",
            CreatedAt = DateTime.UtcNow,

        };
        _context.QueueHistory.Add(history);

        await _context.SaveChangesAsync();
        return token.Id;
    }
}
