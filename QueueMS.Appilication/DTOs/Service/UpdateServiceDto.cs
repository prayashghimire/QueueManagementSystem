
namespace QueueMS.Appilication.DTOs.Service;

public class UpdateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool isActive { get; set; }
}
