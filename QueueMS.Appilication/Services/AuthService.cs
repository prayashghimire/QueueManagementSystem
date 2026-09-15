using Microsoft.AspNetCore.Identity;
using QueueMS.Appilication.DTOs;
using QueueMS.Appilication.Interfaces;
using QueueMS.Domain.Models.UserModels;

namespace QueueMS.Appilication.Services;

public class AuthService : IAuthService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager,  SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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

    public async Task<SignInResult> LoginAsync(LoginRequest loginDto)
    {
        var userLogin = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

        return userLogin;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

  
}
