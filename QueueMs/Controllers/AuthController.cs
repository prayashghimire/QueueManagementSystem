using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueueMS.Appilication.DTOs;
using QueueMS.Appilication.Interfaces;
using QueueMs.Responses;

namespace QueueMs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult>Register(RegisterRequest req)
    {
        var result = await _authService.RegisterAsync(req);
        if (!result.Succeeded)
        {
            return ApiResponses.BadRequest("User Registration Failed");
        }
        return ApiResponses.Ok("User Registered Successfully", result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var result = await _authService.LoginAsync(req);
        if (!result.Succeeded)
        {
            return ApiResponses.BadRequest("User Login Failed");
        }
        return ApiResponses.Ok("User Login Successful", result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return ApiResponses.Ok("User logout Successfully");
    }
}
