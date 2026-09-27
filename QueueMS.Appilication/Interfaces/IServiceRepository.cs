using QueueMS.Domain.Models.ServiceModel;

namespace QueueMS.Appilication.Interfaces;

public interface IServiceRepository
{
    public Task<Services?> GetByIdAsync(int id);
    public Task<List<Services>> GetAllAsync();
    public Task AddAsync (Services service);
    public Task UpdateAsync (Services service);
    public Task DeleteAsync(int id);

}
