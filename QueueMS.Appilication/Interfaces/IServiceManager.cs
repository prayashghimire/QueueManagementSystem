
using QueueMS.Appilication.DTOs.Service;

namespace QueueMS.Appilication.Interfaces;

public interface IServiceManager
{
    Task<ServiceResponseDto> CreateAsync(CreateServiceDto dto);
    Task<ServiceResponseDto?> GetByIdAsync(int id);
    Task<List<ServiceResponseDto>> GetAllAsync();
    Task<ServiceResponseDto?>UpdateAsync(int id, UpdateServiceDto dto);
    Task<bool>DeleteAsync(int id);
}
