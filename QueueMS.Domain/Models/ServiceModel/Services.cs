
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Domain.Models.ServiceModel;

public class Services
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Token> Tokens { get; set; } = new List<Token>();

    public ICollection<CounterService> CounterServices { get; set; } = new List<CounterService>();  


}
