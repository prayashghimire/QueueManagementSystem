using QueueMS.Appilication.DTOs.Token;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.TokenModels;


namespace QueueMS.Appilication.Service;

public class TokenService : ITokenService
{

    private readonly ITokenRepository _tokenRepository;
    private readonly IServiceRepository _serviceRepository;

    public TokenService(ITokenRepository tokenRepository, IServiceRepository serviceRepository)
    {
        _tokenRepository = tokenRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task CancelTokenAsync(int tokenId, int userId)
    {
        var token = await _tokenRepository.GetByIdAsync(tokenId);

        if (token == null) 
            throw new Exception("Token not found");

        if (token.UserId != userId) 
            throw new Exception("You cannot cancel the token");

        if (token.Status != TokenStatus.WAITING) 
            throw new Exception("This token cannot be cancelled");

        token.Status = TokenStatus.CANCELLED;

        await _tokenRepository.UpdateAsync(token);
    }

    public async Task<List<TokenResponse>> GetCustomerTokenAsync(int customerId)
    {
        var token = await _tokenRepository.GetByCustomerIdAsync(customerId);

        return token.Select(t => new TokenResponse
        {
            Id = t.Id,
            TokenNumber = t.TokenNumber,
            ServiceName = t.Service.Name,
            Status = t.Status.ToString(),
            CreatedAt = t.CreatedAt,

        }).ToList();
    }

    public async Task<TokenResponse?> GetTokenAsync(int tokenId)
    {
        var token = await _tokenRepository.GetByIdAsync(tokenId);

        if(token == null) return null;

        return new TokenResponse
        {
            Id=token.Id,
            TokenNumber=token.TokenNumber,
            ServiceName=token.Service.Name,
            Status=token.Status.ToString(),
            CreatedAt=token.CreatedAt,
        };
    }

    public async Task<TokenResponse> TakeTokenAsync(int serviceId, int userId)
    {
        var service = await _serviceRepository.GetByIdAsync(serviceId);

        if (service == null)
        {
            throw new Exception("Service not Found");
        }

        var token = new Token
        {
            ServiceId = serviceId,
            UserId = userId,
            Status = TokenStatus.WAITING,
            CreatedAt = DateTime.UtcNow,

        };

        await _tokenRepository.AddAsync(token);

        return new TokenResponse
        {
            Id = token.Id,
            TokenNumber = token.TokenNumber,
            ServiceName = service.Name,
            Status = token.Status.ToString(),
            CreatedAt = token.CreatedAt,
        };
    }
}
