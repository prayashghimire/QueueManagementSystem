
using QueueMS.Appilication.DTOs.Counter;
using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Appilication.Interfaces;

public interface IQueueRepository
{
    Task<List<CounterWorkloadDto>> GetCounterWorkloadAsync();
    Task<Token?> GetNextTokenAsync(int serviceId);
    Task<int?> CallNextTokenAsync(int counterId, int staffId, int serviceId);

}
