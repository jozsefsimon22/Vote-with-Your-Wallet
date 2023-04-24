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
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        // Checks if the logged in user is an admin
        private async Task<bool> IsUserAdmin(string userId)
        {
            var loggedInUser = await _context.Users.FirstOrDefaultAsync(m => m.Username == Request.Cookies["username"]);

            return loggedInUser != null && loggedInUser.IsAdmin;
        }

        // Constructor initializes the database context
        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Users/Login
        // Renders the login view
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // Validates the user login and sets cookies for the session
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Check if the username and password fields are not empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            // Find the user in the database
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            // If the user is not found, display an error message
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            
            var options = new Microsoft.AspNetCore.Http.CookieOptions();
            options.Expires = DateTime.Now.AddDays(1); // Set the cookie to expire in 1 day

            // Set a cookie to remember the user
            Response.Cookies.Append("Username", user.Username, options);

            // Set a cookie to remember if the user is an admin
            Response.Cookies.Append("Admin", user.IsAdmin.ToString(), options);

            Console.WriteLine(Request.Cookies.ToString());
            if(user.IsAdmin)
            {
                // Redirect the user to the Admin Dashboard after successful login if the user is admin
                return RedirectToAction("Index", "Admin"); 
            }
            // Redirect the user to the CausesList after successful login
            return RedirectToAction("BrowseCauses", "Causes"); 
        }

        // GET: Users/Logout
        // Logs the user out and clears the session cookies
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Admin");
            return RedirectToAction("Index", "Home");
        }

        // GET: Users
        // Displays a list of all users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        // Displays details for a specific user
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            // Find the user in the database
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Username == id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if the logged in user is an admin
            var isAdmin = await IsUserAdmin(user.Username);

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                IsAdmin = isAdmin
            };

            return View(user);
        }

        // GET: Users/Create
        // Renders the user creation view
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Handles the user creation form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,FirstName,LastName")] User user)
        {
            // Check if the submitted form data is valid
            if (ModelState.IsValid)
            {
                // Add the user to the database context and save changes
                _context.Add(user);
                await _context.SaveChangesAsync();
                // Redirect to the users list view
                /*return RedirectToAction(nameof(Index));*/
                return RedirectToAction("Login", "Home");
            }
            // If the form data is not valid, return to the creation view
            return View(user);
        }

        // GET: Users/Edit/5
        // Renders the user edit view for the specified user ID
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            // Find the user in the database
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Handles the user edit form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Email,FirstName,LastName")] User user)
        {
            // If the user ID in the form does not match the specified user ID, return a NotFound result
            if (id != user.Username)
            {
                return NotFound();
            }

            // If the submitted form data is valid, update the user in the database
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // If the user does not exist in the database, return a NotFound result
                    if (!UserExists(user.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to the users list view
                return RedirectToAction(nameof(Index));
            }
            // If the form data is not valid, return to the edit view
            return View(user);
        }

        // GET: Users/Delete/5
        // Renders the user deletion view for the specified user ID
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            // Find the user in the database
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Username == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        // Handles the user deletion form submission
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MyDbContext.Users'  is null.");
            }
            // Find the user in the database
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                // Remove the user from the database context and save changes
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            // Redirect to the users list view
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a user exists in the database
        private bool UserExists(string id)
        {
          return (_context.Users?.Any(e => e.Username == id)).GetValueOrDefault();
        }
    }
}
