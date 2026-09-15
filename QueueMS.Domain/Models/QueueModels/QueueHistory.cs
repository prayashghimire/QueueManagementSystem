namespace QueueMS.Domain.Models.QueueModels;

public class QueueHistory
{
    public int Id { get; set; }
    public int TokenId { get; set; }
    public int CounterId { get; set; }
    public string Action {  get; set; }  = string.Empty;
    public int PerformedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
