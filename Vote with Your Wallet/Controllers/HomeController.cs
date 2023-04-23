using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyDbContext _db;

    public HomeController(ILogger<HomeController> logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BrowseCause()
    {
        var causes = _db.Causes.ToList();
        return View(causes);
    }

    public IActionResult CreateCause()
    {
        var username = Request.Cookies["username"];
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult NewUser()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

