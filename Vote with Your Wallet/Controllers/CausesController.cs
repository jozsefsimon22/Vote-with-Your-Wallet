using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote_with_Your_Wallet.Models;
using Vote_with_Your_Wallet.ViewModels;

namespace Vote_with_Your_Wallet.Controllers
{
    public class CausesController : Controller
    {
        private readonly MyDbContext _context;

        public CausesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Causes/BrowseCauses
        public async Task<IActionResult> BrowseCauses()
        {
            var currentUsername = Request.Cookies["username"];
            var causesWithSignatures = await _context.Causes
                .Include(c => c.User)
                .Where(c => c.User.Username != currentUsername) // Show all causes except the current user's
                .Select(c => new CauseWithSignaturesViewModel
                {
                    Cause = c,
                    SignaturesCount = _context.Signatures.Count(s => s.CauseId == c.ID),
                    SignerNames = _context.Signatures.Where(s => s.CauseId == c.ID).Select(s => s.User.FirstName + " " + s.User.LastName).ToList()
                })
                .ToListAsync();

            return View("CausesList", causesWithSignatures);
        }

        // GET: Causes/MyCausesList
        public async Task<IActionResult> MyCausesList()
        {
            var currentUsername = Request.Cookies["username"];
            var causesWithSignatures = await _context.Causes
                .Include(c => c.User)
                .Where(c => c.User.Username == currentUsername) // Show only the current user's causes
                .Select(c => new CauseWithSignaturesViewModel
                {
                    Cause = c,
                    SignaturesCount = _context.Signatures.Count(s => s.CauseId == c.ID),
                    SignerNames = _context.Signatures.Where(s => s.CauseId == c.ID).Select(s => s.User.FirstName + " " + s.User.LastName).ToList()
                })
                .ToListAsync();
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Login", "Home");
            }

            return View("CausesList", causesWithSignatures);
        }


        // GET: Causes/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Causes.ToListAsync());
        }


        // GET: Causes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // GET: Causes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Causes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CauseName,Description,Date,Username")] Cause cause)
        {
            var currentUsername = Request.Cookies["username"];
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUsername);

            if (currentUser != null)
            {
                cause.Date = DateTime.Now;
                cause.User = currentUser;
                _context.Add(cause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            {
                // Handle the case where the current user is not found
                ModelState.AddModelError("", "Current user not found.");
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // GET: Causes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // POST: Causes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CauseName,Description,Date,Username")] Cause cause)
        {
            if (id != cause.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(cause.ID))
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
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // GET: Causes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // POST: Causes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Causes == null)
            {
                return Problem("Entity set 'MyDbContext.Causes'  is null.");
            }
            var cause = await _context.Causes.FindAsync(id);
            if (cause != null)
            {
                _context.Causes.Remove(cause);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseExists(int id)
        {
          return (_context.Causes?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignCause(int causeId, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Home");
            }

            var user = await _context.Users.FindAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes.FindAsync(causeId);
            if (cause == null)
            {
                return NotFound();
            }

            var existingSignature = await _context.Signatures
                .FirstOrDefaultAsync(s => s.CauseId == causeId && s.Username == username);

            if (existingSignature != null)
            {
                // User has already signed the cause
                return RedirectToAction("BrowseCauses", "Causes");
            }

            var signature = new Signatures
            {
                CauseId = causeId,
                Username = username,
                User = user,
                Cause = cause
            };

            _context.Signatures.Add(signature);
            await _context.SaveChangesAsync();

            return RedirectToAction("BrowseCauses", "Causes");
        }


    }
}
