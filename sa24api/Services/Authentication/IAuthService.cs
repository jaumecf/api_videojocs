﻿using sa24api.Models;

namespace sa24api.Services.Authentication
{
    public interface IAuthService
    {
        public Task<Usuari?> RegisterAsync(UserDto userDto, bool instructor);
        public Task<Usuari?> LoginAsync(UserDto userDto);
        public Task LogoutAsync(int userId);
        public Task<bool> ExistsUserAsync(UserDto userDto);
        public Task<bool> VerifyPasswordHashAsync(UserDto userDto);
    }
}
