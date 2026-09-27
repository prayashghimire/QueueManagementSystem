using Microsoft.AspNetCore.Identity;
using QueueMS.Appilication.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueMS.Appilication.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest loginDto);
        public Task LogoutAsync();
        public Task<IdentityResult> RegisterAsync(RegisterRequest registerDto);
    }
}
