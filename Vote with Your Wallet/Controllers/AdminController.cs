using Microsoft.AspNetCore.Mvc;

namespace Vote_with_Your_Wallet.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
