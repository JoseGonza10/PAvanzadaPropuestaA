using Microsoft.AspNetCore.Mvc;

namespace SiteAsientos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
