
using QueueMS.Appilication.DTOs.Service;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Models.ServiceModel;

namespace QueueMS.Appilication.Service;

public class ServiceManager : IServiceManager
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceManager(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ServiceResponseDto> CreateAsync(CreateServiceDto dto)
    {
        if(string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Service name is required");

        var existingService = await _serviceRepository.GetByNameAsync(dto.Name);

        if(existingService != null)
        {
            throw new InvalidOperationException("Service already exists with same name");
        }

        var service = new Services
        {
            Name = dto.Name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        await _serviceRepository.AddAsync(service);

        return MaptoDto(service, 0);
    }

    public async Task<ServiceResponseDto?>GetByIdAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if(service == null)
            return null;

        var averageServiceTime = await _serviceRepository.GetAverageServiceTimeAsync(id);

        return MaptoDto(service, averageServiceTime);
    }

    public async Task<List<ServiceResponseDto>> GetAllAsync()
    {
        var services = await _serviceRepository.GetAllAsync();

        var averageServiceTime = await _serviceRepository.GetAverageServiceTimeAsync();

        return services
            .Select(s => MaptoDto(s, averageServiceTime.TryGetValue(s.Id, out var average) ? average : 0))
            .ToList();
    }

    public async Task<ServiceResponseDto?>UpdateAsync(int id, UpdateServiceDto dto)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            return null;

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Service name is required");

        var existingService = await _serviceRepository.GetByNameAsync(dto.Name);

        if (existingService != null && existingService.Id != id)
            throw new InvalidOperationException("Service with this name already exists");

        service.Name = dto.Name;
        service.IsActive = dto.isActive;

        await _serviceRepository.UpdateAsync(service);

        var averageServiceTime = await _serviceRepository.GetAverageServiceTimeAsync(id);

        return MaptoDto(service, averageServiceTime);
    }

    public async Task<bool>DeleteAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if(service == null) return false;

        service.IsActive = false;

        await _serviceRepository.UpdateAsync(service);
        return true;
    }

    private static ServiceResponseDto MaptoDto(Services service, double averageServiceTime)
    {
        return new ServiceResponseDto
        {
            Id = service.Id,
            Name = service.Name,
            AvergeServiceTime = averageServiceTime,
            isActive = service.IsActive,
            
        };
    }
}
