using Microsoft.EntityFrameworkCore;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.TokenModels;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly QueueMSDatabaseContext _context;

    public TokenRepository(QueueMSDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Token?> GetByIdAsync(int id)
    {
        return await _context.Tokens.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Token>> GetWaitingTokenAsync(int serviceId)
    {
        return await _context.Tokens
            .Where(x => x.ServiceId == serviceId && x.Status==TokenStatus.WAITING)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Token>> GetByCustomerIdAsync(int userId)
    {
        return await _context.Tokens
            .Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<int> GetNextTokenNumberAsync(int serviceId, DateOnly tokenDate)
    {

        var counter = await _context.TokenCounter
                        .FirstOrDefaultAsync(x => x.ServiceId == serviceId && x.TokenDate == tokenDate);

        if (counter == null)
        {
            counter = new TokenCounter
            {
                ServiceId = serviceId,
                TokenDate = tokenDate,
                LastNumber = 1

            };

            _context.TokenCounter.Add(counter);
        }
        else
            counter.LastNumber += 1;

        await _context.SaveChangesAsync();

        return counter.LastNumber;
                           
    }
    
    public async Task AddAsync(Token token)
    {
        await _context.Tokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Token token)
    {
        _context.Tokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task CancelAsync(int tokenId)
    {
        var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Id == tokenId);

        if (token == null) return;

        token.Status = TokenStatus.CANCELLED;
        await _context.SaveChangesAsync();
    }

  
}
