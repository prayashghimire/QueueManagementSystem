using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Appilication.Interfaces;

public interface ITokenRepository
{
    public Task<Token?>GetByIdAsync(int id);
    public Task<List<Token>> GetByCustomerIdAsync(int userId);
    public Task<List<Token>> GetWaitingTokenAsync(int serviceId);
    public Task AddAsync(Token token);
    public Task UpdateAsync (Token token);
    public Task CancelAsync(int tokenId);
}
