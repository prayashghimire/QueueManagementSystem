
using QueueMS.Domain.Models.ServiceModel;

namespace QueueMS.Domain.Models.TokenModels;

public class TokenCounter
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public Services Services { get; set; } = null!;
    public DateOnly TokenDate { get; set; }
    public int LastNumber { get; set; }
}
