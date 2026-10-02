
namespace QueueMS.Appilication.Interfaces;

public interface IRolesManager
{
    Task AssignStaffAsync(int userId);
    Task AssignStaffToCounterAsync(int userId, int counterId);
    
}
