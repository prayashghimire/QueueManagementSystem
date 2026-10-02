
using QueueMS.Domain.Models.CounterModels;

namespace QueueMS.Appilication.Interfaces;

public interface ICounterStaffRepository
{
    Task<bool> ExistsAsync(int userId, int counterId);
    Task AddAsync(CounterStaff counterStaff);
}
