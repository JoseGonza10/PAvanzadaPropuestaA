using Microsoft.AspNetCore.Mvc;

namespace SiteAsientos.Controllers
{
    public class Plantilla2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
