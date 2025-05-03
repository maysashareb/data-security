using System;
using System.Security.Claims;
using System.Threading.Tasks;
using data_security.Data;
using data_security.Models;
using data_security.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_security.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public AccountController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = null;

            try
            {
                // First attempt: SQL Injection vulnerable query
                string query = $"SELECT * FROM Users WHERE Username = '{model.Username}' AND PasswordHash = '{model.Password}'";
                user = await _context.Users.FromSqlRaw(query).FirstOrDefaultAsync();

                if (user == null)
                {
                    using var sha256 = System.Security.Cryptography.SHA256.Create();
                    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                    var passwordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                    user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == passwordHash);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't show it to the user
                Console.WriteLine($"SQL Error: {ex.Message}");
                // Try normal login as fallback
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                var passwordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == passwordHash);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            var claims = new[] 
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", user.IsAdmin ? "Admin" : "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result)
                {
                    TempData["SuccessMessage"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "Username already exists or registration failed.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isVerified = await _userService.VerifyUserEmailAsync(model.Username, model.Email);

                if (isVerified)
                {
                    return RedirectToAction("ResetPassword", new { username = model.Username });
                }

                ModelState.AddModelError(string.Empty, "The username and email combination was not found.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string username)
        {
            var model = new ResetPasswordViewModel { Username = username };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model.Username, model.NewPassword);

                if (result)
                {
                    TempData["SuccessMessage"] = "Password has been reset successfully. Please log in with your new password.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "Failed to reset password. User not found.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}