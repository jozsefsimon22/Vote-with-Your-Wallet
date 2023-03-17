﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BrowseCause()
    {
        return View();
    }

    public IActionResult CreateCause()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult MyCauses()
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

