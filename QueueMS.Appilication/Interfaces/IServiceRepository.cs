using QueueMS.Domain.Models.ServiceModel;

namespace QueueMS.Appilication.Interfaces;

public interface IServiceRepository
{
     Task<Services?> GetByIdAsync(int id);
     Task<List<Services>> GetAllAsync();
    Task<Services?> GetByNameAsync(string name);
    Task<double> GetAverageServiceTimeAsync(int id);
    Task<Dictionary<int, double>> GetAverageServiceTimeAsync();
     Task AddAsync (Services service);
     Task UpdateAsync (Services service);
     Task DeleteAsync(int id);

}
