using System.Collections.Generic;
using System.Threading.Tasks;
using data_security.Models;

namespace data_security.Services
{
    public interface IUserService
    {
        // Existing methods
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<bool> VerifyUserEmailAsync(string username, string email);
        Task<bool> ResetPasswordAsync(string username, string newPassword);
        Task<bool> UsernameExistsAsync(string username);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> IsAdminAsync(int userId);

        // New methods for SQL injection demonstration
        Task<User> AuthenticateWithSqlInjectionAsync(string username, string password);
        Task<User> AuthenticateWithStoredProcedureAsync(string username, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync();
    }
}