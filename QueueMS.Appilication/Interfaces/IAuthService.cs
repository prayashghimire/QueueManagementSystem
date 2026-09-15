using Microsoft.AspNetCore.Identity;
using QueueMS.Appilication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueMS.Appilication.Interfaces
{
    public interface IAuthService
    {
        public Task<SignInResult> LoginAsync(LoginRequest loginDto);
        public Task LogoutAsync();
        public Task<IdentityResult> RegisterAsync(RegisterRequest registerDto);
    }
}
