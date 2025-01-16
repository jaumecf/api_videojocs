using sa24api.Models;

namespace sa24api.Services.Users
{
    public interface IUsersService
    {
        public Task<Usuari> InsertUserAsync(Usuari user);
        public Task<Usuari?> GetUserAsync(string userEmail);
    }
}
