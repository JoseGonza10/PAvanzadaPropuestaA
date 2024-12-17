using Microsoft.AspNetCore.Mvc;

namespace SiteAsientos.Controllers
{
	public class PlantillaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Billing()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Profile() 
        {
            return View();
        }

        public IActionResult Tables() 
        {
            return View();
        }

        public IActionResult SignIn() 
        {
            return View();
        }
    }
}
