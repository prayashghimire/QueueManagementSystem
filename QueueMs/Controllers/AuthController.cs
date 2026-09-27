using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueueMS.Appilication.Interfaces;
using QueueMs.Responses;
using Microsoft.AspNetCore.Authorization;
using QueueMS.Appilication.DTOs.Auth;

namespace QueueMs.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult>Register(RegisterRequest req)
    {
        var result = await _authService.RegisterAsync(req);
        if (!result.Succeeded)
        {
            return ApiResponses.BadRequest("User Registration Failed");
        }
        return ApiResponses.Ok("Registered successfully", result);
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        try
        {
            var result = await _authService.LoginAsync(req);
            return ApiResponses.Ok("User Login Successful", result);
        }catch(UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return ApiResponses.Ok("User logout Successfully");
    }
}
