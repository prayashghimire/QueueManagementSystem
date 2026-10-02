using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Appilication.Interfaces;

public interface ITokenRepository
{
     Task<Token?>GetByIdAsync(int id);
     Task<List<Token>> GetByCustomerIdAsync(int userId);
     Task<List<Token>> GetWaitingTokenAsync(int serviceId);
     Task<int> GetNextTokenNumberAsync(int serviceId, DateOnly tokenDate);
     Task AddAsync(Token token);
     Task UpdateAsync (Token token);
     Task CancelAsync(int tokenId);
}
