
using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Domain.Models.CounterModels;

public class Counter 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool isActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Token> Tokens { get; set; } = new List<Token>();
    public ICollection<CounterService> CounterServices { get; set; } = 
        new List<CounterService>();
    public ICollection<CounterStaff> CounterStaffs { get; set; } = new List<CounterStaff>();

}
