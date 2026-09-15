using QueueMS.Domain.Models.UserModels;

namespace QueueMS.Domain.Models.CounterModels;

public class CounterStaff
{
    public int CounterId { get; set; }
    public int StaffId { get; set; }

    public Counter Counter { get; set; } = null!;
    public User Staff   { get; set; } = null!;
}
