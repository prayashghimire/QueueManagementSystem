namespace QueueMS.Domain.Enum;

public enum TokenStatus
{

    WAITING,
    CALLED,
    SERVING,
    COMPLETED,
    SKIPPED,
    CANCELLED
}
