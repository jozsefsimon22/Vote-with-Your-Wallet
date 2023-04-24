using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Controllers;

public class HomeController : Controller
{
    // These fields are used to log information and access the application's database.
    private readonly ILogger<HomeController> _logger;
    private readonly MyDbContext _db;

    // This constructor injects the ILogger and MyDbContext objects into the controller.
    public HomeController(ILogger<HomeController> logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    // This action method returns the home page view.
    public IActionResult Index()
    {
        return View();
    }

    // This action method returns the "browse causes" view with a list of all causes from the database.
    public IActionResult BrowseCause()
    {
        var causes = _db.Causes.ToList();
        return View(causes);
    }

    // This action method returns the "create cause" view, but only if the user is logged in.
    // Otherwise, it redirects to the login page.
    public IActionResult CreateCause()
    {
        var username = Request.Cookies["username"];
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    // This action method returns the login page view.
    public IActionResult Login()
    {
        return View();
    }

    // This action method returns the "new user" registration page view.
    public IActionResult NewUser()
    {
        return View();
    }

    // This action method returns the error page view.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

