using Microsoft.AspNetCore.Identity;
using QueueMS.Appilication.DTOs.Auth;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Models.UserModels;

namespace QueueMS.Appilication.Service;

public class AuthService : IAuthService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenService _tokenService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }


    public async Task<IdentityResult> RegisterAsync(RegisterRequest registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if(existingUser != null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Email already Exists"
                });
        }

        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        return result;
        
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null) throw new UnauthorizedAccessException("Invalid Email");

        var userLogin = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password, lockoutOnFailure: false);

        if (!userLogin.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var (token, expiresAt) = _tokenService.CreateToken(user, roles);



        return new LoginResponse
        {
            Id = user.Id,
            Email = user.Email!,
            Roles = roles,
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

  
}
