using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Domain.Models.QueueModels;
using QueueMS.Domain.Models.ServiceModel;
using QueueMS.Domain.Models.UserModels;

namespace QueueMS.Domain.Models.TokenModels;

public class Token
{
    public int Id { get; set; }
    public string TokenNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public TokenStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CalledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? CounterId { get; set; }

    public Services Service { get; set; } = null!;
    public User Customer { get; set; } = null!;
    public Counter? Counter { get; set; }
    public ICollection<QueueHistory> QueueHistories { get; set; } = new List<QueueHistory>();
}
