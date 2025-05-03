using data_security.Models;
using data_security.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace data_security.Controllers
{
    public class SqlInjectionController : Controller
    {
        private readonly IUserService _userService;

        public SqlInjectionController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /SqlInjection
        public IActionResult Index()
        {
            return View();
        }

        // Basic SQL Injection Method
        [HttpGet]
        public IActionResult BasicInjection()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BasicInjection(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Use the vulnerable method with direct string concatenation
            var user = await _userService.AuthenticateWithSqlInjectionAsync(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            ViewBag.Message = $"Successfully logged in as {user.Username} with ID {user.UserID}. Admin status: {user.IsAdmin}";

            // If the injection was successful in granting admin access, show all user data
            if (user.IsAdmin)
            {
                var allUsers = await _userService.GetAllUsersAsync();
                var allCreditCards = await _userService.GetAllCreditCardsAsync();

                ViewBag.AllUsers = allUsers;
                ViewBag.AllCreditCards = allCreditCards;
            }

            return View("InjectionResult");
        }

        // Advanced SQL Injection Method (Stored Procedure)
        [HttpGet]
        public IActionResult AdvancedInjection()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AdvancedInjection(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Use the vulnerable method with stored procedure
            var user = await _userService.AuthenticateWithStoredProcedureAsync(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            ViewBag.Message = $"Successfully logged in as {user.Username} with ID {user.UserID}. Admin status: {user.IsAdmin}";

            // If the injection was successful in granting admin access, show all user data
            if (user.IsAdmin)
            {
                var allUsers = await _userService.GetAllUsersAsync();
                var allCreditCards = await _userService.GetAllCreditCardsAsync();

                ViewBag.AllUsers = allUsers;
                ViewBag.AllCreditCards = allCreditCards;
            }

            return View("InjectionResult");
        }
    }
}