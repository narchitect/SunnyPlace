using Microsoft.AspNetCore.Mvc;

namespace SunnyPlace
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
