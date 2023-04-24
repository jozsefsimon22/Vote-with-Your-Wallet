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
            // Retrieve all causes except the current user's and populate a view model with the cause, the number of signatures, and the names of the signers
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
            // Retrieve the current user's causes and populate a view model with the cause, the number of signatures, and the names of the signers
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
            // Retrieve all causes and display them in the Index view (the default view for this controller)
            return View(await _context.Causes.ToListAsync());
        }


        // GET: Causes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Retrieve a cause by ID and display its details
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
            // Display the Create view
            return View();
        }

        // POST: Causes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CauseName,Description,Date,Username")] Cause cause)
        {
            // Get the current user's username from cookies
            var currentUsername = Request.Cookies["username"];
            // Find the current user in the database
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUsername);

            // If the current user exists, create the cause and save it to the database
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
            // Populate the ViewData with a list of usernames for the SelectList
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // GET: Causes/Edit/5
        // This action method retrieves a cause to be edited
        public async Task<IActionResult> Edit(int? id)
        {
            // If the cause ID is null, return a NotFound result
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            // Find the cause in the database
            var cause = await _context.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            // Populate the ViewData with a list of usernames for the SelectList
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // POST: Causes/Edit/5
        // This action method updates an existing cause
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CauseName,Description,Date,Username")] Cause cause)
        {
            // If the cause ID does not match the edited cause ID, return a NotFound result
            if (id != cause.ID)
            {
                return NotFound();
            }

            // If the model state is valid, update the cause in the database
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
                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            // Populate the ViewData with a list of usernames for the SelectList
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", cause.Username);
            return View(cause);
        }

        // GET: Causes/Delete/5
        // This action method retrieves a cause to be deleted
        public async Task<IActionResult> Delete(int? id)
        {
            // If the cause ID is null, return a NotFound result
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }
            // Find the cause in the database and include the associated user
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
        // This action method confirms the deletion of a cause
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // If the context causes set is null, return an error message
            if (_context.Causes == null)
            {
                return Problem("Entity set 'MyDbContext.Causes'  is null.");
            }
            // Find the cause in the database
            var cause = await _context.Causes.FindAsync(id);
            // If the cause is found, remove it from the database
            if (cause != null)
            {
                _context.Causes.Remove(cause);
            }
            // Save changes to the database
            await _context.SaveChangesAsync();
            // Redirect to the Index action
            return RedirectToAction(nameof(Index));
        }

        // Check if a cause exists in the database based on its ID
        private bool CauseExists(int id)
        {
          return (_context.Causes?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // POST: Sign a cause
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignCause(int causeId, string username)
        {
            // If the username is null or empty, redirect to the Login action
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Home");
            }

            // Find the user in the database
            var user = await _context.Users.FindAsync(username);

            // If the user is not found, return a NotFound result
            if (user == null)
            {
                return NotFound();
            }

            // Find the cause in the database
            var cause = await _context.Causes.FindAsync(causeId);

            // If the cause is not found, return a NotFound result
            if (cause == null)
            {
                return NotFound();
            }

            // Check if the user has already signed the cause
            var existingSignature = await _context.Signatures
                .FirstOrDefaultAsync(s => s.CauseId == causeId && s.Username == username);

            // If the user has already signed the cause, redirect to the BrowseCauses action
            if (existingSignature != null)
            {
                // User has already signed the cause
                return RedirectToAction("BrowseCauses", "Causes");
            }

            // Create a new signature object
            var signature = new Signatures
            {
                CauseId = causeId,
                Username = username,
                User = user,
                Cause = cause
            };

            // Add the signature to the database
            _context.Signatures.Add(signature);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the BrowseCauses action
            return RedirectToAction("BrowseCauses", "Causes");
        }


    }
}
