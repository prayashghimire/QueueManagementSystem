using QueueMS.Domain.Models.ServiceModel;

namespace QueueMS.Domain.Models.CounterModels;

public class CounterService
{
    public int CounterId { get; set; }
    public int ServiceId { get; set; }


    public Counter Counters { get; set; } = null!;
    public Services Services { get; set; } = null!;
}
