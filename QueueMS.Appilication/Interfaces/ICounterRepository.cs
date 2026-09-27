
using QueueMS.Domain.Models.CounterModels;

namespace QueueMS.Appilication.Interfaces;

public interface ICounterRepository
{
    Task<Counter?>GetByIdAsync(int id);
    Task<List<Counter>> GetActiveCounterAsync();
    Task<List<Counter>>GetCounterForServiceAsync(int serviceId);
    Task<Counter?> GetHighestWorkloadCounterAsync();
    Task AddAsync(Counter counter);
    Task UpdateAsync(Counter counter);
}
