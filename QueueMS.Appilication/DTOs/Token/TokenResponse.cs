namespace QueueMS.Appilication.DTOs.Token;

public class TokenResponse
{
    public int Id { get; set; }
    public string TokenNumber { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
