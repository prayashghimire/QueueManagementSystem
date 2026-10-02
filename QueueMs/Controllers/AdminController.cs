using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueMs.Responses;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Role;

namespace QueueMs.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]
public class AdminController :ControllerBase
{
    private IRolesManager _rolesManager;

    public AdminController(IRolesManager rolesManager)
    {
        _rolesManager = rolesManager;
    }

    [HttpPost("users/{userId:int}/assign-staff")]
    public async Task<IActionResult> AssignStaffRole(int userId)
    {
        await _rolesManager.AssignStaffAsync(userId);

        return ApiResponses.Ok("Staff Assigned Successfully", userId);

    }

    [HttpPost("counters/{counterId:int}/assign-staff/{staffId:int}")]
    public async Task<IActionResult>AssignStaffToCounter(int staffId, int counterId)
    {
        await _rolesManager.AssignStaffToCounterAsync(staffId, counterId);

        return ApiResponses.Ok("Staff Assigned to counter", staffId);
    }
}
