
using Microsoft.AspNetCore.Identity;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Domain.Models.UserModels;
using QueueMS.Domain.Role;

namespace QueueMS.Appilication.Service;

public class RolesManager : IRolesManager
{
    private readonly ICounterStaffRepository _counterStaffRepository;
    private readonly UserManager<User> _userManager;
    private readonly ICounterRepository _counterRepository;

    public RolesManager(ICounterStaffRepository counterStaffRepository, UserManager<User> userManager, ICounterRepository counterRepository)
    {
        _counterStaffRepository = counterStaffRepository;
        _userManager = userManager;
        _counterRepository = counterRepository;
    }

    public async Task AssignStaffAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new Exception("User Cannot be found");


        if (await _userManager.IsInRoleAsync(user, Roles.Staff))
            throw new Exception("User is already a staff");

        var result = await _userManager.AddToRoleAsync(user, Roles.Staff);

        if (!result.Succeeded)
            throw new Exception("User Assignment Failed");

    }

    public async Task AssignStaffToCounterAsync(int userId, int counterId)
    {
        var staff = await _userManager.FindByIdAsync(userId.ToString());

        if (staff == null)
            throw new Exception("User cannot be Found");

        if (!await _userManager.IsInRoleAsync(staff, Roles.Staff))
            throw new Exception("User is not a staff");

        var counter = await _counterRepository.GetByIdAsync(counterId);

        if (counter == null)
            throw new Exception($"Counter not found {counterId}");

        var alreadyAssigned = await _counterStaffRepository.ExistsAsync(userId, counterId);

        if (alreadyAssigned)
            throw new Exception("Staff Already Assigned to counter");

        await _counterStaffRepository.AddAsync(
            new CounterStaff
            {
                StaffId = userId,
                CounterId = counterId,
            });
    }
}
