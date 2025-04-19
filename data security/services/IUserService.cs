using System.Threading.Tasks;
using data_security.Models;

namespace data_security.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<bool> VerifyUserEmailAsync(string username, string email);
        Task<bool> ResetPasswordAsync(string username, string newPassword);
        Task<bool> UsernameExistsAsync(string username);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> IsAdminAsync(int userId);
    }
}