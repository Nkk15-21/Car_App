using Microsoft.AspNetCore.Mvc;

namespace Car_App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Cars");
        }
    }
}
