
namespace QueueMS.Appilication.DTOs.Counter;

public class CounterWorkloadDto
{
    public int CounterId { get; set; }
    public string CounterName { get; set; } = string.Empty;
    public int WaitingCustomerCount { get; set; }
    public bool isActive { get; set; }

}
