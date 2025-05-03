using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using data_security.Data;
using data_security.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace data_security.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Original secure authentication method
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            if (user.PasswordHash != HashPassword(password))
                return null;

            return user;
        }

        // VULNERABLE METHOD 1: SQL Injection through string concatenation
        public async Task<User> AuthenticateWithSqlInjectionAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            string hashedPassword = HashPassword(password);
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // VULNERABLE: Direct string concatenation - SQL Injection vulnerability
                string query = "SELECT * FROM Users WHERE Username = '" + username + "' AND PasswordHash = '" + hashedPassword + "'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                                Email = reader["Email"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // VULNERABLE METHOD 2: Using stored procedure with dynamic SQL
        public async Task<User> AuthenticateWithStoredProcedureAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            string hashedPassword = HashPassword(password);
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // This is assuming a stored procedure called "sp_AuthenticateUser"
                using (SqlCommand command = new SqlCommand("EXECUTE('SELECT * FROM Users WHERE Username = ''''' + @Username + ''''' AND PasswordHash = ''''' + @PasswordHash + '''''');", connection))
                {
                    // VULNERABLE: Parameters without proper escaping 
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                                Email = reader["Email"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Get all users including their credit card information
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.CreditCard)
                .ToListAsync();
        }

        // Get all credit cards
        public async Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync()
        {
            return await _context.CreditCards.ToListAsync();
        }

        // Existing methods from your original UserService
        public async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                return false;

            bool isFirstUser = !await _context.Users.AnyAsync();

            var user = new User
            {
                Username = model.Username,
                PasswordHash = HashPassword(model.Password),
                Email = model.Email,
                IsAdmin = isFirstUser // First user = admin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyUserEmailAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Email == email);
        }

        public async Task<bool> ResetPasswordAsync(string username, string newPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            user.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.CreditCard)
                .FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task<bool> IsAdminAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null && user.IsAdmin;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}