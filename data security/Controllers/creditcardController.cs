using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using data_security.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using data_security.Data;

namespace data_security.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditCardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditCard
        public async Task<IActionResult> Index()
        {
            var creditCards = await _context.CreditCards
                .Include(c => c.User)
                .ToListAsync();
            return View(creditCards);
        }

        // GET: CreditCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // GET: CreditCard/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: CreditCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,IdNumber,CardNumber,ValidDate,CVC,UserId")] CreditCard creditCard)
        {
            // Check if the credit card is expired
            if (!string.IsNullOrEmpty(creditCard.ValidDate))
            {
                if (DateTime.TryParseExact(creditCard.ValidDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expiryDate))
                {
                    // Get the last day of the expiry month
                    var lastDayOfMonth = new DateTime(expiryDate.Year, expiryDate.Month, DateTime.DaysInMonth(expiryDate.Year, expiryDate.Month));

                    if (lastDayOfMonth < DateTime.Now)
                    {
                        ModelState.AddModelError("ValidDate", "Credit card has expired");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(creditCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", creditCard.UserId);
            return View(creditCard);
        }

        // GET: CreditCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards.FindAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", creditCard.UserId);
            return View(creditCard);
        }

        // POST: CreditCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,IdNumber,CardNumber,ValidDate,CVC,UserId")] CreditCard creditCard)
        {
            if (id != creditCard.Id)
            {
                return NotFound();
            }

            // Check if the credit card is expired for edit as well
            if (!string.IsNullOrEmpty(creditCard.ValidDate))
            {
                if (DateTime.TryParseExact(creditCard.ValidDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expiryDate))
                {
                    // Get the last day of the expiry month
                    var lastDayOfMonth = new DateTime(expiryDate.Year, expiryDate.Month, DateTime.DaysInMonth(expiryDate.Year, expiryDate.Month));

                    if (lastDayOfMonth < DateTime.Now)
                    {
                        ModelState.AddModelError("ValidDate", "Credit card has expired");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCardExists(creditCard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", creditCard.UserId);
            return View(creditCard);
        }

        // GET: CreditCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditCard = await _context.CreditCards.FindAsync(id);
            _context.CreditCards.Remove(creditCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardExists(int id)
        {
            return _context.CreditCards.Any(e => e.Id == id);
        }
    }
}