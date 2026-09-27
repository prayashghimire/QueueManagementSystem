using QueueMS.Appilication.DTOs.Token;

namespace QueueMS.Appilication.Interfaces;

public interface ITokenService
{
    public Task<TokenResponse> TakeTokenAsync(int serviceId, int customerId);

    public Task<TokenResponse?> GetTokenAsync(int TokenId);
    Task<List<TokenResponse>> GetCustomerTokenAsync(int customerId);
    Task CancelTokenAsync(int TokenId, int CustomerId);
}
