using QueueMS.Domain.Models.UserModels;

namespace QueueMS.Appilication.Interfaces;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAt) CreateToken(User user, IEnumerable<string> role);
}
